using System.Text;

abstract class WordProcessor {
  char[] separators;

  public WordProcessor(char[] separators) {
    this.separators = separators;
  }

  abstract protected void ProcessWord(string word);

  abstract public void Print();

  public void ProcessFile(string file) { 
    var currentWord = new StringBuilder(16);
    using(var reader = new StreamReader(file)) { 
      int charCode = reader.Read();
      while(charCode != -1) {
        char character = (char)charCode;
        if (separators.Contains(character)) {
          if (currentWord.ToString() != "") ProcessWord(currentWord.ToString());
          currentWord.Clear();
        } else { 
          currentWord.Append(character);
        }
        charCode = reader.Read();
      }
      if (currentWord.ToString() != "") ProcessWord(currentWord.ToString());
    }
  }

}

class WordCounter : WordProcessor {
  public int Count { get; private set; } = 0;
  public WordCounter(char[] separators) : base(separators) { }
  protected override void ProcessWord(string word) {
    Count++;
  }
  public override void Print() {
    Console.WriteLine(Count);
  }
}

class FrequencyCounter : WordProcessor {
  public Dictionary<string, int> WordFrequencies { get; private set; }
    = new Dictionary<string, int>();
  public FrequencyCounter(char[] separators) : base(separators) { }
  protected override void ProcessWord(string word) {
    if (!WordFrequencies.ContainsKey(word)) 
      WordFrequencies[word] = 1;
    else
      WordFrequencies[word]++;
  }
  public override void Print() {
    foreach(var word in WordFrequencies.Keys.Order())
      Console.WriteLine("{0}: {1}", word, WordFrequencies[word]);
  }
}

class Program {
  public static void Main(string[] args) {
    try { 
      if (args.Length != 1) throw new Exception("Argument Error");
      var wordCounter = new WordCounter(new[] { ' ', '\t', '\n' });
      wordCounter.ProcessFile(args[0]);
      wordCounter.Print();

      var freqCounter = new FrequencyCounter(new[] { ' ', '\t', '\n' });
      freqCounter.ProcessFile(args[0]);
      freqCounter.Print();
    } catch(Exception e) {
      Console.WriteLine(e.Message);
    }
  }
}
