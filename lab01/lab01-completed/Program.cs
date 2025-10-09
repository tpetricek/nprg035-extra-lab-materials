using Lab01;

class Program {
  public static void Main(string[] args) {
    // DifferentTypesOfValues.Run();
    // UpdateObjectInstance.Run();
    // RecursiveVariableDeclaration.Run();
    // VariableSizes.Run();
  }
}

#region 01 - DifferentTypesOfValues

public class DifferentTypesOfValues { 
  public static void Run()
  {
    // Kolik objektu bude naalokovano na halde (heap)?
    // Kolik hodnot bude ulozeno na zasobniku (stack)?
    Random rnd = new Random();
    int start = rnd.Next(0, 100);
    int end = rnd.Next(start, 100);
    Range rng = new Range();
    rng.Start = start;
    rng.End = end;
    string message = rng.ToString();
    Console.WriteLine(message);
  }
}

struct Range
{
  public int Start;
  public int End;
  public override string ToString() {
    return $"{Start}..{End}";
  }
}

#endregion
#region 02 - UpdateObjectInstance 

class UpdateObjectInstance {
  public static void Run() { 
    S s = new S();
    s.X = 10;
    Update(s);
		Console.WriteLine(s.X);
  }

  private static void Update(S s) {
      s.X = 20;
  }
}

#endregion
#region 03 - RecursiveVariableDeclaration

class A { }

class RecursiveVariableDeclaration
{
  static string Generate1(int n) {
    if (n == 0) return "";

    string s1 = n.ToString();
    string r = Generate1(n - 1);
    return s1 + "," + r;
  }

  static string s2;

  static string Generate2(int n) {
    if (n == 0) return "";

    s2 = n.ToString();
    string r = Generate2(n - 1);
    return s2 + "," + r;
  }


  public static void Run()
  {
    Console.WriteLine(Generate1(10));
    Console.WriteLine(Generate2(10));
  }
}

#endregion
#region 04 - VariableSizes

public record class C
{
  int Index;
  decimal Open;
  decimal High;
  decimal Low;
  decimal Close;
}

public class VariableSizes
{
  public static void Run()
  {
    Loop(0); 
  }

  public static void Loop(int n)
  {
    // Za jak dlouho nam dojde pamet?
    C c = new C();
    Console.Title = $"Iteration {n} ({c})";
    Loop(n + 1);
  }
}

#endregion