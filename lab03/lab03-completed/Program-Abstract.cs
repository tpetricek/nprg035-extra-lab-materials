using System.Reflection.PortableExecutable;
using System.Text;

interface ITokenReader {
  Token ReadToken();
}

interface ITokenProcessor {
  void ProcessToken(Token token);
  void ProcessEnd();
}

class WordReader(TextReader reader) : ITokenReader {
  private readonly char[] whitespace = [ ' ', '\n', '\t' ];
  private bool IsWhitespace(char c) {
    foreach(var w in whitespace) 
      if (w == c) return true;
    return false;
  }

  StringBuilder currentWord = new StringBuilder();

  public Token ReadToken() {
    throw new NotImplementedException();
  }
}

/// <summary>
/// Reader for a CSV-like file format. It sums the data in the specified 'colName'.
/// </summary>
public class CsvLikeReader {
  StreamReader reader;
  string colName;
  int foundIndex;
  public long Sum { get; private set; }

  public CsvLikeReader(StreamReader reader, string colName) {
    this.reader = reader;
    this.colName = colName;
  }

  public void ReadHeader() {
    throw new NotImplementedException();
  }

  public void ReadData() {
    throw new NotImplementedException();
  }
}

public class AbstractSumOrNotSum {
  public static void AbstractMain(string[] args) {
    //
    // TODO: This is still missing the correct exception handling!
    //
    string column = args[2];
    using(var sr = new StreamReader(args[0])) {
      var csv = new CsvLikeReader(sr, args[2]);
      csv.ReadHeader();
      csv.ReadData();

      Console.WriteLine(args[2]);
      for(var i=0; i<args[2].Length; i++) Console.Write('-');
      Console.WriteLine();
      Console.WriteLine(csv.Sum);
    }
  }
}