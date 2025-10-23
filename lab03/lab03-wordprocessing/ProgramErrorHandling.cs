using System.Reflection.PortableExecutable;
using System.Text;
using System.IO;

public class SumOrNotSum {
  public static void SumDataInColumn(StreamReader sr, string colName) {
    // NOTE: This is still extremely ugly and will get you 0 points!
    int foundIndex = -1;
    int colIndex = 0;
    StringBuilder col = new StringBuilder();
    int character = sr.Read();
    while(character != '\n' && character != -1) {
      if (character == ' ' || character == '\t') {
        if (col.Length > 0) {
          if (col.ToString() == colName) foundIndex = colIndex;
          colIndex++;
        }
        col.Clear();
      } else {
        col.Append((char)character);
      }
      character = sr.Read();
    }
    if (col.Length > 0) {
      if (col.ToString() == colName) foundIndex = colIndex;
      colIndex++;
    }

    col = new StringBuilder();
    long sum = 0L;
    while(character != -1) { 
      colIndex = 0;
      while(character != '\n' && character != -1) {
        if (character == ' ' || character == '\t') {
          if (col.Length > 0) {
            if (colIndex == foundIndex) sum+=Int64.Parse(col.ToString());
            colIndex++;
          }
          col.Clear();
        } else {
          col.Append((char)character);
        }
        character = sr.Read();
      }
      if (col.Length > 0) {
        if (colIndex == foundIndex) sum+=Int64.Parse(col.ToString());
        colIndex++;
      }
      character = sr.Read();
    }

    Console.WriteLine(colName);
    for(var i=0; i<colName.Length; i++) Console.Write('-');
    Console.WriteLine();
    Console.WriteLine(sum);
  }

  /// <summary>
  /// Runs the count or not task and throws CountOrNotException
  /// if the arguments are wrong or file does not exist.
  /// </summary>
  public static void RunTask(string[] args) {
    if (args.Length != 3) throw CountOrNotError.ArgumentError;
    try { 
      string column = args[2];
      using(var sr = new StreamReader(args[0])) {
        SumDataInColumn(sr, column);
      }
    } catch (FileNotFoundException) {
      throw CountOrNotError.FileError;
    }
  }

  public static void Main(string[] args) {
    try {
      RunTask(args);
    } catch (CountOrNotError e) {
      Console.WriteLine(e.Message);
    } 
  }
}
class CountOrNotError : Exception {
  public CountOrNotError(string message) : base(message) { }

  public static CountOrNotError ArgumentError {
    get { return new CountOrNotError("Argument Error"); }
  }
  public static CountOrNotError FileError {
    get { return new CountOrNotError("File Error"); }
  }
}