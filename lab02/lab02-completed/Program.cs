using System.Text;

class Program {
  public static string Root { get; } = 
    @"C:\Tomas\Academic\teaching\nprg035\public\lab02\";
  public static void Main(string[] args) {
    //Program1.Run();
    //Program2.Run();
    //Program3.Run();
    //Program4.Run();
    //Program5.Run();
    Program6.Run();
  }
}

#region Word Count - One-liner

class Program1 {
  public static void Run() {
    var txt = File.ReadAllText(Program.Root + "sample.txt");
    var words = txt.Split(new[] { ' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
    Console.WriteLine(words.Length);
  }
}

#endregion
#region Word Frequency - One-liner

class Program2 {
  public static void Run() {
    var txt = File.ReadAllText(Program.Root + "sample.txt");
    var words = txt.Split(new[] { ' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
    var dict = new Dictionary<string, int>();
    foreach (var word in words) { 
      if (!dict.ContainsKey(word)) dict[word] = 0;
      dict[word]++;
    }
    foreach(var k in dict.Keys.Order())
      Console.WriteLine("{0}: {1}", k, dict[k]);
  }
}

#endregion
#region Word Count - Using ReadLine

class Program3 { 
  public static void Run() {
    var fs = new StreamReader(Program.Root + "sample.txt");
    var line = fs.ReadLine();
    var count = 0;
    while (line != null) {
      var words = line.Split(new[] { ' ', '\t', '\n'}, StringSplitOptions.RemoveEmptyEntries);
      count += words.Length;
      line = fs.ReadLine();
    }
    Console.WriteLine(count);
  }
}

#endregion
#region Word Count - Using Read

class Program4 { 
  public static void Run() {
    var count = 0;
    var sb = new StringBuilder();
    using(var fs = new StreamReader(Program.Root + "sample.txt")) { 
      var read = fs.Read();
      while (read != -1) {
        var ch = (char)read;
        if (ch == ' ' || ch == '\t' || ch == '\n')
        {
          if (sb.Length > 0) count++;
          sb.Clear();
        }
        else
        {
          sb.Append(ch);
        }
        read = fs.Read();
      }
      if (sb.Length > 0) count++;
      Console.WriteLine(count);
    }
  }
}

#endregion
#region Word Count - Standalone class

class WordReader {
  char[] whitespace;
  public WordReader(char[] whitespace) {
    this.whitespace = whitespace;
  }
  private bool IsWhitespace(char c) {
    return whitespace.Contains(c);
  }

  public void ProcessFile(string file)
  {
    var totalCount = 0;
    var currentWord = new StringBuilder();
    using(var fs = new StreamReader(file)) { 
      var readChar = fs.Read();
      while (readChar != -1) {
        var ch = (char)readChar;
        if (IsWhitespace(ch)) {
          if (currentWord.Length > 0) totalCount++;
          currentWord.Clear();
        }
        else {
          currentWord.Append(ch);
        }
        readChar = fs.Read();
      }
      if (currentWord.Length > 0) totalCount++;
      Console.WriteLine(totalCount);
    }
  }
}

class Program5 {
  public static void Run()   {
    var wr = new WordReader(new[] { ' ', '\t', '\n' });
    wr.ProcessFile(Program.Root + "sample.txt");
  }
}
#endregion
#region Word Count - OOP

abstract class AbstractWordReader {
  char[] whitespace;
  public AbstractWordReader(char[] whitespace) {
    this.whitespace = whitespace;
  }
  private bool IsWhitespace(char c) {
    return whitespace.Contains(c);
  }
  protected abstract void ProcessWord(string word);

  public void ProcessFile(string file)
  {
    var currentWord = new StringBuilder();
    using(var fs = new StreamReader(file)) { 
      var readChar = fs.Read();
      while (readChar != -1) {
        var ch = (char)readChar;
        if (IsWhitespace(ch)) {
          if (currentWord.Length > 0) 
            ProcessWord(currentWord.ToString());
          currentWord.Clear();
        }
        else {
          currentWord.Append(ch);
        }
        readChar = fs.Read();
      }
      if (currentWord.Length > 0) 
        ProcessWord(currentWord.ToString());  
    }
  }
}

class WordCounter : AbstractWordReader {
  public WordCounter(char[] whitespace) : base(whitespace) { }
  public int WordCount { get; private set; }
  protected override void ProcessWord(string word) {
    WordCount++;
  }
}

class FrequencyCounter : AbstractWordReader
{
  public FrequencyCounter(char[] whitespace) : base(whitespace) { }
  public Dictionary<string, int> WordCounts { get; private set; } = 
    new Dictionary<string, int>();
  protected override void ProcessWord(string word) {
    if (!WordCounts.ContainsKey(word)) WordCounts[word] = 0;
    WordCounts[word]++;
  }

}

class Program6 {
  public static void Run()   {
    var wc = new WordCounter(new[] { ' ', '\t', '\n' });
    wc.ProcessFile(Program.Root + "sample.txt");
    Console.WriteLine(wc.WordCount);

    var fc = new FrequencyCounter(new[] { ' ', '\t', '\n' });
    fc.ProcessFile(Program.Root + "sample.txt");
    foreach(var k in fc.WordCounts.Keys.Order())
      Console.WriteLine("{0}: {1}", k, fc.WordCounts[k]);
  }
}

#endregion
