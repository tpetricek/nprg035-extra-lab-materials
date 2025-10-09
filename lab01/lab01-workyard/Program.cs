using Lab01;

class Program {

  public static void Main(string[] args) {
    // DifferentTypesOfValues.Run();
    // UpdateObjectInstance.Run();
    // RecursiveVariableDeclaration.Run();
    // VariableSizes.Run();
    // BinaryTrees.Run();
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
    var a = new int[] { 1, 2, 3 };
  }
}

class Range
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
    // TODO
		Console.WriteLine(s.X);
  }

  private static void Update(S s) {
    // TODO
  }
}

#endregion
#region 03 - RecursiveVariableDeclaration

class RecursiveVariableDeclaration
{
  static string Generate1(int n) {
    if (n == 0) return "";

    string s1 = n.ToString();
    string r = Generate1(n - 1);
    return s1 + "," + r;
  }

  // TODO: Where else can we store 's1'?

  public static void Run()
  {
    Console.WriteLine(Generate1(10));
    // Console.WriteLine(Generate2(10));
  }
}

#endregion
#region 04 - VariableSizes

public record struct Price
{
  public int Index;
  public decimal Open;
  public decimal High;
  public decimal Low;
  public decimal Close;
}

public class VariableSizes
{
  public static void Run()
  {
    // Za jak dlouho nam dojde zasobnik?
    Loop(0); 
  }

  public static void Loop(int n)
  {
    // TODO
  }
}

#endregion
#region 05 - BinaryTree
public record class Node {
  public int Value;
  public Node Left;
  public Node Right;
}

public static class BinaryTrees
{ 
  public static int Sum(Node n)
  {
    return n.Value + Sum(n.Left) + Sum(n.Right);
  }

  public static void Run() 
  {
    var a = new Node() {  Left = null, Right = null, Value = 10 };
    var b = new Node() {  Left = null, Right = a, Value = 32 };

    // TODO: What happens when we run this?
    Console.WriteLine(Sum(b));
  }
}

#endregion
