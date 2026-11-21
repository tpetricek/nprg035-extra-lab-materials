using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordModules  {
  class WordCounter : IWordProcessor {
    int currentCount = 0;
    public void ProcessWord(string word) {
      currentCount++;
    }
    public void ProcessEnd() {
      Console.WriteLine(currentCount);
    }
  }
  class FrequencyCounter : IWordProcessor {
    Dictionary<string, int> frequencies = new Dictionary<string, int>();
    public void ProcessWord(string word) {
      if (!frequencies.ContainsKey(word)) frequencies[word] = 1;
      else frequencies[word]++;
    }
    public void ProcessEnd() {
      foreach(var word in frequencies.Keys.Order())
        Console.WriteLine($"{word}: {frequencies[word]}");
    }
  }
}
