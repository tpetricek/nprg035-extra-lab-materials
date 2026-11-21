using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordModules;

namespace WordModules {
  class TextWordReader(TextReader reader) : IWordReader {  
    StringBuilder currentWord = new StringBuilder();
    public string? ReadWord() {
      while(true) { 
        int currentCharCode = reader.Read();
        if (currentCharCode == -1) {
          if (currentWord.Length > 0) { 
            string word = currentWord.ToString();
            currentWord.Clear();
            return word;
          }
          else {
            return null;
          }
        }
        char currentChar = (char)currentCharCode;
        if (currentChar is ' ' or '\n' or '\t') { 
          if (currentWord.Length > 0) { 
            string word = currentWord.ToString();
            currentWord.Clear();
            return word;
          }
        } else { 
          currentWord.Append(currentChar);
        }
      }
    }
  }
}
