using System.Text;

class Program {
  public static string Sample { get; } = 
    @"C:\Tomas\Academic\teaching\nprg035\public\lab02\sample.txt";
  public static void Main(string[] args) {
    Program1.Run();
    Program2.Run();
    Program3.Run();
    Program4.Run();
    Program5.Run();
    Program6.Run();
  }
}

#region Word Count - One-liner

class Program1 {
  public static void Run() {
    Console.WriteLine(File.ReadAllText(Program.Sample).Split(['\n','\t',' ']).Length);
  }
}

#endregion
#region Word Frequency - One-liner

class Program2 {
  public static void Run() {
    var dict = new Dictionary<string, int>();
    foreach(var w in File.ReadAllText(Program.Sample).Split(['\n','\t',' '])) {
      if (!dict.ContainsKey(w)) dict.Add(w, 0);
      dict[w] = dict[w] + 1;
    }
    foreach(var k in dict.Keys.Order()) { 
      Console.WriteLine($"{k}: {dict[k]}");
    }
  }
}

#endregion
#region Word Count - Using ReadLine

class Program3 { 
  public static void Run() {
    using(var reader = File.OpenText(Program.Sample)) {
      string? line;
      int count = 0;
      while(null != (line = reader.ReadLine())) 
        count += line.Split(['\n','\t',' ']).Length;
      Console.WriteLine(count);
    }
  }
}

#endregion
#region Word Count - Using Read

class Program4 { 
  public static void Run() {
    try {
      using(var reader = File.OpenText(Program.Sample) {
        int input;
        int count = 0;
        StringBuilder word = new StringBuilder();
        while((input = reader.Read()) != -1) {
          char chr = (char)input;
          if (chr == '\n' || chr == '\t' || chr == ' ') {
            if (word.ToString() != "") count++;
            word.Clear();
          } else {
            word.Append(chr);
          }
        } 
        if (word.ToString() != "") count++;
        Console.WriteLine(count);
      }
    } catch { 
      Console.WriteLine("File Error");
    }
  }
}

#endregion
#region Word Count - Standalone class

class FileWordCounter(char[] whitespace) {
  private bool IsWhitespace(char c) {
    foreach(var w in whitespace) 
      if (w == c) return true;
    return false;
  }

  public int ProcessFile(string file) {
    int totalCount = 0;
    using(var reader = File.OpenText(file)) {
      int input;
      StringBuilder word = new StringBuilder();
      while((input = reader.Read()) != -1) {
        char chr = (char)input;
        if (IsWhitespace(chr)) {
          if (word.ToString() != "") totalCount++;
          word.Clear();
        } else {
          word.Append(chr);
        }
      } 
      if (word.ToString() != "") totalCount++;
    }
    return totalCount;
  }
}

class Program5 {
  public static void Run()   {
    try {
      var wp = new FileWordCounter([' ','\n','\t']);
      int count = wp.ProcessFile(args[0]);
      Console.WriteLine(count);
    } catch { 
      Console.WriteLine("File Error");
    }
  }
}
#endregion
#region Word Count - Classes
interface IWordReader {
  string? ReadWord();
}

interface IWordProcessor {
  void ProcessWord(string word);
  void ProcessEnd();
}

class WordReader(char[] whitespace, TextReader reader) : IWordReader {
  private bool IsWhitespace(char c) {
    foreach(var w in whitespace) 
      if (w == c) return true;
    return false;
  }

  StringBuilder currentWord = new StringBuilder();

  public string? ReadWord() {
    while(true) { 
      int input = reader.Read();
      if (input == -1) {
        if (currentWord.ToString() != "") { 
          var word = currentWord.ToString();
          currentWord.Clear();
          return word; 
        } 
        else return null;
      }
      char chr = (char)input;
      if (IsWhitespace(chr)) {
        if (currentWord.ToString() != "") { 
          var word = currentWord.ToString();
          currentWord.Clear();
          return word; 
        } 
      } else {
        currentWord.Append(chr);
      }
    }
  }
}

class WordCounter : IWordProcessor {
  private int count = 0;
  public void ProcessWord(string word) { count++; }
  public void ProcessEnd() {
    Console.WriteLine(count);
  }
}

class WordFrequencyCounter : IWordProcessor {
  private Dictionary<string, int> counts = new Dictionary<string, int>();
  public void ProcessWord(string word) {
    if (!counts.ContainsKey(word)) counts.Add(word, 0);
    counts[word]++; 
  }
  public void ProcessEnd() {
    foreach(var word in counts.Keys.Order()) {
      Console.WriteLine($"{word}: {counts[word]}");
    }
  }
}


class Program6 {
  public static void ProcessWords(IWordReader wr, IWordProcessor wp) {
    string? word;
    while((word = wr.ReadWord()) != null) {
      wp.ProcessWord(word);
    }
    wp.ProcessEnd();
  }

  public static void Run()   {
    try {
      using(var reader = File.OpenText(Program.Sample)) { 
        var wr = new WordReader([' ','\n','\t'], reader);
        var wp = new WordFrequencyCounter();
        ProcessWords(wr, wp);
      }
    } catch { 
      Console.WriteLine("File Error");
    }
  }
}

#endregion
