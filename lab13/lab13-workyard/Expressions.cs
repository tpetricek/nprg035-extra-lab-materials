namespace Expressions;

// Represents any expression
public abstract class Expression { }

// DEMOS - Creating and evaluating some expressions

public class Demos {
  public static void Run() {
    Expression e1 = null; // 42
    Console.WriteLine(e1); // evaluate!

    Expression e2 = null; // 40 + 2
    Console.WriteLine(e1); // evaluate!
  }
}