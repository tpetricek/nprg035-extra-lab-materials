using System.Text;
namespace Reader;

class Program {
  public static string Sample { get; } = 
    @"C:\Tomas\Academic\teaching\nprg035\public\lab02\sample.txt";
  public static void Main(string[] args) {
    Program6.Run();
  }
}

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
