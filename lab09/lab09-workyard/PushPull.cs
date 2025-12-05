namespace PushPull;

#region Pull model

public interface IWordGenerator {
  public string? ReadNextWord();
}

public class HelloGenerator : IWordGenerator {
  int state = -1;
  public string? ReadNextWord() {
    state++;
    // TODO: Emit strings
  }
}

public class PullDemo {
  public static void Run() {
    var gen = new HelloGenerator();
    string? word;
    while((word = gen.ReadNextWord()) != null) {
      // TODO: Consume strings
    }
  }
}

#endregion
#region Push model 

public interface IWordReceiver {
  public void AcceptWord(string word);
}

public class WordReceiver : IWordReceiver {
  public void AcceptWord(string word) {
    // TODO: Consume strings
  }
}

public class PushDemo {
  public static void PushHelloWorld(IWordReceiver rec) {
    // TODO: Emit strings
  }
  public static void Run() { 
    PushHelloWorld(new WordReceiver());
  }
}

#endregion 