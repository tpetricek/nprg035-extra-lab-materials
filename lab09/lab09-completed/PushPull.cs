namespace PushPull;

#region Pull model

public interface IWordGenerator {
  public string? ReadNextWord();
}

public class HelloGenerator : IWordGenerator {
  int state = -1;
  public string? ReadNextWord() {
    state++;
    if (state == 0) return "Hello";
    else if (state == 1) return "World";
    else return null;
  }
}

public class PullDemo {
  public static void Run() {
    var gen = new HelloGenerator();
    string? word;
    while((word = gen.ReadNextWord()) != null) Console.WriteLine(word + " ");
  }
}

#endregion
#region Push model 

public interface IWordReceiver {
  public void AcceptWord(string word);
}

public class WordReceiver : IWordReceiver {
  public void AcceptWord(string word) {
    Console.WriteLine(word + " ");
  }
}

public class PushDemo {
  public static void PushHelloWorld(IWordReceiver rec) {
    rec.AcceptWord("Hello");
    rec.AcceptWord("World");
  }
  public static void Run() { 
    PushHelloWorld(new WordReceiver());
  }
}

#endregion 