namespace Expressions;

// Represents any expression
public abstract class Expression {
  public abstract int Evaluate();
}

// Represents operators
// (we need this only later when parsing)
public abstract class OperatorExpression : Expression {
}

// Represents binary operators
// (they only need to provide operation on numbers)
public abstract class BinaryOperatorExpression(Expression left, Expression right) : OperatorExpression {
  public Expression Left { get; } = left;
  public Expression Right { get; } = right;
  public override int Evaluate() {
    return EvaluateBinary(Left.Evaluate(), Right.Evaluate());
  }
  abstract public int EvaluateBinary(int left, int right);
}

// An example of a concrete binary operator
public class BinaryPlusExpression(Expression left, Expression right)
    : BinaryOperatorExpression(left, right) {
  public override int EvaluateBinary(int left, int right) {
    return left + right;
  }
}

// Represents values 
// (constants, variable accesses, etc.)
public class ValueExpression : Expression {
  virtual public int Value { get; }
  public override int Evaluate() {
    return Value;
  }
}

public class ConstantExpression(int value) : ValueExpression {
  public override int Value => value;
}

// DEMOS - Creating and evaluating some expressions

public class Demos {
  public static void Run() {
    // 
    Expression e1 = new ConstantExpression(42);
    Console.WriteLine(e1.Evaluate());

    Expression e2 = new BinaryPlusExpression(
      new ConstantExpression(40),
      new ConstantExpression(2));
    Console.WriteLine(e2.Evaluate());
  }
}