using System.Reflection.PortableExecutable;
using System.Text;

/// <summary>
/// Reads a line from the specified 'reader' and calls the 
/// 'ProcessWord' method that is implemented in the derived class
/// </summary>
public abstract class LineReader {
  StreamReader reader;
  public LineReader(StreamReader reader) {
    this.reader = reader;
  }

  public abstract void ProcessWord(string word);
  
  public int ProcessLine() {
    StringBuilder col = new StringBuilder();
    int character = reader.Read();
    while(character != '\n' && character != -1) {
      if (character == ' ' || character == '\t') {
        if (col.Length > 0) {
          ProcessWord(col.ToString());
        }
        col.Clear();
      } else {
        col.Append((char)character);
      }
      character = reader.Read();
    }
    if (col.Length > 0) {
      ProcessWord(col.ToString());
    }
    return character;
  }
}

/// <summary>
/// Reads the header line from the input file and computes
/// the index of the given 'colName'
/// </summary>
public class HeaderReader : LineReader {
  string colName;
  int colIndex;
  public int FoundIndex { get; private set; }

  public HeaderReader(StreamReader reader, string colName) : base(reader) { 
    this.colName = colName;
    this.FoundIndex = - 1;
    this.colIndex = 0;
  }

  public override void ProcessWord(string word) {
    if (word == colName) FoundIndex = colIndex;
    colIndex++;
  }
}

/// <summary>
/// Reads the data line from the input file and adds the 
/// value at the specified index to the 'Sum'. The reader
/// can be used to process multiple lines by using 'NextLine'
/// </summary>
public class DataReader : LineReader {
  int foundIndex;
  int colIndex;
  /// <summary>
  /// Returns the computed sum so far
  /// </summary>
  public long Sum { get; private set; }
  public DataReader(StreamReader reader, int foundIndex) : base(reader) {
    this.foundIndex = foundIndex;
    this.Sum = 0;
  }
  /// <summary>
  /// Reset the reader so that it can process the next line of the input
  /// </summary>
  public void NextLine() {
    colIndex = 0;
  }
  public override void ProcessWord(string word) {
    if (colIndex == foundIndex) Sum += Int64.Parse(word);
    colIndex++;
  }
}

/// <summary>
/// Reader for a CSV-like file format. It sums the data in the specified
/// 'colName'. You use this by calling 'ReadHeader' once and 'ReadData' then.
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
    var hreader = new HeaderReader(reader, colName);
    hreader.ProcessLine();
    foundIndex = hreader.FoundIndex;
  }

  public void ReadData() {
    var dreader = new DataReader(reader, foundIndex);
    var last = dreader.ProcessLine(); 
    while(last != -1) {
      dreader.NextLine();
      last = dreader.ProcessLine();
    }
    Sum = dreader.Sum;
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