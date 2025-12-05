using TokenProcessingFramework;

// RUN the main demo!
Demo.Run();

// Representing paragraph lines (similar approach to Tokens)

public enum LineKind { NormalLine, LastLine, Paragraph }
public record class ParagraphLine(List<string> Words, LineKind Kind);

// IParagraphLineProcessor can process paragraph lines in some way
// We have an implementation that just prints the line (ParagraphLineDebugger)
// and an ITokenProcessor that detects lines and calls IParagraphLineProcessor
// (this one is just a concrete class, not an interface)

public interface IParagraphLineProcessor {
  void ProcessLine(ParagraphLine line);
}

public class ParagraphLineSplitter(int MaxLength, IParagraphLineProcessor processor) : ITokenProcessor {
  
  List<string> currentWords = new List<string>();
  int currentLength = 0;
  private void EmitParagraph() {
    processor.ProcessLine(new ParagraphLine(new(), LineKind.Paragraph));
  }

  private void EmitLine(bool last) {
    if ( currentLength > 0) {
      processor.ProcessLine(new ParagraphLine(currentWords, last ? LineKind.LastLine : LineKind.NormalLine));
      currentWords.Clear();
      currentLength = 0;
    }
  }
  private void AddWord(string word) {
    currentWords.Add(word);
    currentLength += word.Length + 1;
  }

  public void Finish() {
    EmitLine(true);
  }

  public void ProcessToken(Token token) {
    if (token.Type == TokenType.Word) {
      if (currentLength + 1 + token.Value!.Length > MaxLength) {
        EmitLine(false);     
      } 
      AddWord(token.Value);
    }
    else if (token.Type == TokenType.EndOfParagraph) {
      EmitLine(true);
      EmitParagraph();
    }
  }
}

public class ParagraphLineDebugger : IParagraphLineProcessor {
  public void ProcessLine(ParagraphLine line) {
    if (line.Kind == LineKind.Paragraph) 
      Console.WriteLine();
    else
      Console.WriteLine("{0} ({1})", String.Concat(line.Words.Select(s => s + " ")), line.Kind);
  }
}

// ILineJustifier can print a justfied line
// We have an implementation for left and block 

public interface ILineJustifier {
  void WriteLine(List<string> line);
}

public class LineLeftJustifier(TextWriter writer) : ILineJustifier {
  public void WriteLine(List<string> line) {
    for (int i = 0; i < line.Count; i++) {
      if (i != 0) writer.Write(' ');
      writer.Write(line[i]);
    }
    writer.WriteLine();
  }
}

public class LineBlockJustifier(TextWriter writer, int targetWidth) : ILineJustifier {
  public void WriteLine(List<string> line) {
		if (line.Count == 0) writer.WriteLine();
		else if (line.Count == 1) writer.WriteLine(line[0]);
		else {
      int totalWordsWidth = line.Sum(w => w.Length);
			int spacesPerLine = targetWidth - totalWordsWidth - (line.Count - 1);
			int spacesPerWord = 1 + spacesPerLine / (line.Count - 1);
			int surplusSpaces = spacesPerLine % (line.Count - 1);
			for (int i = 0; i < line.Count; i++) {
				if (i != 0) {
          int spaces = spacesPerWord + (surplusSpaces-- > 0 ? 1 : 0);
          for (int j = 0; j < spaces; j++) writer.Write(' ');
				}
				writer.Write(line[i]);
			}
			writer.WriteLine();
		}
  }
}

// Putting it all together - this IParagraphLineProcessor will print
// internal lines using one justifier and last lines using another

public class ParagraphLinePrinter(TextWriter writer, ILineJustifier normalLine, ILineJustifier lastLine) : IParagraphLineProcessor {
  public void ProcessLine(ParagraphLine line) {
    if (line.Kind == LineKind.Paragraph) 
      writer.WriteLine();
    else if (line.Kind == LineKind.LastLine) 
      lastLine.WriteLine(line.Words);
    else if (line.Kind == LineKind.NormalLine)  
      normalLine.WriteLine(line.Words);
  }
}

// Various steps of the demo...

public static class Demo {
  public static void Run1() {
    ITokenReader reader = new ByLinesTokenReader(new StreamReader("test.txt"));
    Token token;
    while((token = reader.ReadToken()).Type != TokenType.EndOfInput) {
      Console.WriteLine(token.ToString());
    }
  }

  public static void Run2() {
    ITokenReader reader = new ParagraphDetectingTokenReaderDecorator(new ByLinesTokenReader(new StreamReader("test.txt")));
    Token token;
    while((token = reader.ReadToken()).Type != TokenType.EndOfInput) {
      Console.WriteLine(token.ToString());
    }
  }

  public static void Run3() {
    ITokenReader reader = new ParagraphDetectingTokenReaderDecorator(new ByLinesTokenReader(new StreamReader("test.txt")));
    IParagraphLineProcessor proc = new ParagraphLineDebugger();
    ITokenProcessor splitter = new ParagraphLineSplitter(20, proc);
    
    Token token;
    while((token = reader.ReadToken()).Type != TokenType.EndOfInput) {
      splitter.ProcessToken(token);
    }
  }

  public static void Run() {
    ITokenReader reader = new ParagraphDetectingTokenReaderDecorator(new ByLinesTokenReader(new StreamReader("test.txt")));
    IParagraphLineProcessor proc = new ParagraphLinePrinter(Console.Out, new LineBlockJustifier(Console.Out, 40), new LineLeftJustifier(Console.Out));
    ITokenProcessor splitter = new ParagraphLineSplitter(40, proc);
    
    Token token;
    while((token = reader.ReadToken()).Type != TokenType.EndOfInput) {
      splitter.ProcessToken(token);
    }
  }
}
