using System.Reflection.PortableExecutable;
using System.Text;

public class UglySumOrNotSum {
  public static void UglyMain(string[] args) {
    string column = args[2];
    using(var sr = new StreamReader(args[0])) {
      int foundIndex = -1;
      int colIndex = 0;
      StringBuilder col = new StringBuilder();
      int character = sr.Read();
      while(character != '\n' && character != -1) {
        if (character == ' ' || character == '\t') {
          if (col.Length > 0) {
            if (col.ToString() == args[2]) foundIndex = colIndex;
            colIndex++;
          }
          col.Clear();
        } else {
          col.Append((char)character);
        }
        character = sr.Read();
      }
      if (col.Length > 0) {
        if (col.ToString() == args[2]) foundIndex = colIndex;
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

      Console.WriteLine(args[2]);
      for(var i=0; i<args[2].Length; i++) Console.Write('-');
      Console.WriteLine();
      Console.WriteLine(sum);
    }
  }
}