// Demo1.Run();
// Demo3.Run();
// Demo4.Run(); 
// Demo5.Run(); 

#region Demo #1 - Virtual methods and interface methods

using System.Globalization;

class A {
	public virtual void m() { Console.WriteLine("A.m() code"); 	}
}

class B : A {
	public override void m() { Console.WriteLine("B.m() code"); }
}

interface I1 {
	public void m();
}

class X : I1 {
	public void m() { Console.WriteLine("X.m() code"); }
}

class Y : X, I1 {
	public new void m() { Console.WriteLine("Y.m() code"); }
}

class Z : I1 {
	public void m() { Console.WriteLine("Z.m() code"); }
}

public class Demo1 {
	public static void Run() {
		A a = new B();
		a.m();

		X x = new Y();
		x.m();
	}
}

#endregion
#region Demo #2 - Abstract methods and classes

public abstract class FileProcessor {
    public void Process() {
        ReadFile();
        TransformData();
				InitializeFile();
        WriteFile();
    }

    protected abstract void InitializeFile();
    protected abstract void ReadFile();
    protected abstract void TransformData();
    protected abstract void WriteFile(); 
}

#endregion
#region Demo #3 - IWordReader and IWordProcessor

public interface IWordReader {
	public abstract string? ReadWord();
}

public interface IWordProcessor {
	public abstract void ProcessWord(string word);
	public abstract void PrintResult();
}

public record class SimpleWordReader(string input) : IWordReader {
	string[] words = input.Split([' ','\n'], StringSplitOptions.RemoveEmptyEntries);
	int index = 0;
  public string? ReadWord() {
    if (index >= words.Length) return null;
		return words[index++];
  }
}

public record class SimpleWordCounter : IWordProcessor {
  public int Count { get; private set; } = 0;
  public void PrintResult() {
    Console.WriteLine(Count);
  }
  public void ProcessWord(string word) {
    Count++;
  }	
}

public class Demo3 {
	public static void Run() {
		string sample = "Hello world \n this is my test";
		IWordReader wr = new SimpleWordReader(sample);
		IWordProcessor wp = new SimpleWordCounter();
		ProcessAll(wr, wp);	
	}
	public static void ProcessAll(IWordReader wr, IWordProcessor wp) {
		string? word = wr.ReadWord();
		while(word != null) {
			wp.ProcessWord(word);
			word = wr.ReadWord();
		}
		wp.PrintResult();
	}
}

#endregion
#region Demo #4 - Adapting IWordProcessor 

public enum TokenKind { Word, Newline }
public record class Token(TokenKind Kind, string Value);

public interface ITokenReader {
	public abstract Token? ReadToken();
}

public interface ITokenProcessor {
	public abstract void ProcessToken(Token token);
	public abstract void PrintResult();
}

public record class TokenWordProcessorAdapter(IWordProcessor wp) : ITokenProcessor {
  public void PrintResult() {
    wp.PrintResult();
  }  
	public void ProcessToken(Token token) {
		if (token.Kind == TokenKind.Word)
			wp.ProcessWord(token.Value);
  }
}
public record class SimpleTokenReader(string input) : ITokenReader {
	string[] words = input.Split([' '], StringSplitOptions.RemoveEmptyEntries);
	int index = 0;
  public Token? ReadToken() {
    if (index >= words.Length) return null;
		string word = words[index++];
		if (word == "\n") return new Token(TokenKind.Newline, word);
		else return new Token(TokenKind.Word, word);
  }
}

public class Demo4 {
	public static void Run() {
		string sample = "Hello world \n this is my test";
		ITokenReader wr = new SimpleTokenReader(sample);
		ITokenProcessor wp = new TokenWordProcessorAdapter(new SimpleWordCounter());
		ProcessAll(wr, wp);	
	}
	public static void ProcessAll(ITokenReader wr, ITokenProcessor wp) {
		Token? token = wr.ReadToken();
		while(token != null) {
			wp.ProcessToken(token);
			token = wr.ReadToken();
		}
		wp.PrintResult();
	}
}

#endregion
#region Demo #5 - Double.Parse is trickier than you think!

class Demo5 { 
	public static void Run() {
		// CurrentCulture determines if floating point numbers
		// use comma or dot as the decimal separator!
		
		// 42,0 => 42 and 42.0 => exception
		Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("cs-CZ");

		// 42,0 => 420 and 42.0 => 42
		Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

		Console.WriteLine(Double.Parse("42.0"));
		Console.WriteLine(Double.Parse("42,0"));
	}
}

#endregion
