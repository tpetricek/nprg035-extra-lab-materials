using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordModules {
  interface IWordReader {
    public string? ReadWord();
  }
  interface IWordProcessor {
    public void ProcessWord(string word);
    public void ProcessEnd();
  }
}
