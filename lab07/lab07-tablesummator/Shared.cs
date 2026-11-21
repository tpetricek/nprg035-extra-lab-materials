namespace TableSummator;

public enum TokenType { EndOfInput = 0, Word, EndOfLine, EndOfParagraph }

public readonly record struct Token(TokenType Type, string? Value = null) {
	public Token(string word) : this(TokenType.Word, word) { }
}

public interface ITokenProcessor {
	public void ProcessToken(Token token);
	public void Finish();	
}

