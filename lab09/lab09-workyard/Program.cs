using System.Text;
using TokenProcessingFramework;

Demo.Run();

public static class Demo {
  public static void Run() {
    ITokenReader reader = new ByLinesTokenReader(new StreamReader("test.txt"));
    Token token;
    while((token = reader.ReadToken()).Type != TokenType.EndOfInput) {
      Console.WriteLine(token.ToString());
    }
  }
}