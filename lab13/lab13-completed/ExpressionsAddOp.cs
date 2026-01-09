namespace ExpressionsAddOp;

// Represents any expression
public abstract class Expression {
  public abstract int Evaluate();
}

// Represents operators
// (we need this only later when parsing)
public abstract class OperatorExpression : Expression {
  public abstract bool AddOperand(Expression op);
}

// Represents binary operators
// (they only need to provide operation on numbers)
public abstract class BinaryOperatorExpression : OperatorExpression {
  public Expression? Left { get; private set; }
  public Expression? Right { get; private set; }
  public override int Evaluate() {
    return EvaluateBinary(Left!.Evaluate(), Right!.Evaluate());
  }
  abstract public int EvaluateBinary(int left, int right);
  
  public override bool AddOperand(Expression op) {
    if (Left == null) { Left = op; return true; }
    if (Right == null) { Right = op; return true; }
    return false;
  }
}

// An example of a concrete binary operator
public class BinaryPlusExpression : BinaryOperatorExpression {
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

    OperatorExpression e2 = new BinaryPlusExpression();
    e2.AddOperand(new ConstantExpression(40));
    e2.AddOperand(new ConstantExpression(2));
    Console.WriteLine(e2.Evaluate());
  }
}