using System.Reflection.PortableExecutable;
using System.Text;

public enum TokenType { Word, NewLine, Whitespace, EndOfFile }

public struct Token {
  public TokenType Type;
  public string Value;
}
  
public class Tokenizer {
  StreamReader reader;
  public Tokenizer(StreamReader reader) {
    this.reader = reader;
  }
  private bool IsWhite(int character) {
    return character == ' ' || character == '\t';
  }
  private bool IsNewline(int character) {
    return character == '\n';
  }
  private bool IsNotSpecial(int character) {
    return !IsWhite(character) && !IsNewline(character) && character != -1;
  }

  public Token ReadNext() {
    StringBuilder tok = new StringBuilder();
    int character = reader.Peek();
    if (character == -1) {
      reader.Read();
      return new Token() { Type = TokenType.EndOfFile, Value = "" }; 
    }
    if (IsNewline(character)) {
      reader.Read();
      return new Token() { Type = TokenType.NewLine, Value = "\n" }; 
    }
    if (IsWhite(character)) {
      while (IsWhite(character)) {
        tok.Append((char)character);
        reader.Read();
        character = reader.Peek();
      }
      return new Token() { Type = TokenType.Whitespace, Value = tok.ToString() }; 
    }
    while (IsNotSpecial(character)) {
      tok.Append((char)character);
        reader.Read();
        character = reader.Peek();
    }
    return new Token() { Type = TokenType.Word, Value = tok.ToString() }; 
  }
}

public class TokenSumOrNotSum {
  public static void Main(string[] args) {
    string column = args[2];
    using(var sr = new StreamReader(args[0])) {
      Tokenizer tokenizer = new Tokenizer(sr);
      Token token = tokenizer.ReadNext();
      while(token.Type != TokenType.EndOfFile) {
        Console.WriteLine("{0}: {1}", token.Type, token.Value.Trim());
        token = tokenizer.ReadNext();
      }
    }
  }
}