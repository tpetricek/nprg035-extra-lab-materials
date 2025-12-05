using System;
namespace TokenProcessingFramework;

#nullable enable

public enum TokenType { EndOfInput = 0, Word, EndOfLine, EndOfParagraph }

public readonly record struct Token(TokenType Type, string? Value = null) {
	public Token(string word) : this(TokenType.Word, word) { }
}
public interface ITokenProcessor {
	public void ProcessToken(Token token);
	public void Finish();
}

public interface ITokenReader {
	public Token ReadToken();
}

public class ByLinesTokenReader : ITokenReader, IDisposable {
	private static readonly char[] Whitespaces = new[] { ' ', '\t' };

	private readonly TextReader _reader;
	private string[] _words = Array.Empty<string>();
	private int _nextWord = 0;
	private bool _endOfLineReported = true;

	public ByLinesTokenReader(TextReader reader) {
		_reader = reader;
	}

	public Token ReadToken() {
		while (_nextWord >= _words.Length) {
			if (!_endOfLineReported) {
				_endOfLineReported = true;
				return new Token(TokenType.EndOfLine);
			}

			if (_reader.ReadLine() is string line) {
				_words = line.Split(Whitespaces, StringSplitOptions.RemoveEmptyEntries);
				_nextWord = 0;
				_endOfLineReported = false;
			} else {
				return new Token(TokenType.EndOfInput);
			}
		}

		return new Token(TokenType.Word, _words[_nextWord++]);
	}

	public void Dispose() {
		_reader.Dispose();
	}
}

public record class ParagraphDetectingTokenReaderDecorator(ITokenReader Reader) : ITokenReader {
	private bool _firstParagraphStarted = false;
	private Token? _nextToken = null;

	public Token ReadToken() {
		if (_nextToken is not null) {
			var token = _nextToken.Value;
			_nextToken = null;
			return token;
		} else {
			int newLinesFound = 0;

			Token token;
			while ((token = Reader.ReadToken()).Type == TokenType.EndOfLine) {
				newLinesFound++;
			}

			if ((newLinesFound > 1 && _firstParagraphStarted) || token.Type == TokenType.EndOfInput) {
				_nextToken = token;
				return new Token(TokenType.EndOfParagraph);
			} else {
				_firstParagraphStarted = true;
				return token;
			}
		}
	}
}
