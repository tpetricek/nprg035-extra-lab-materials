Demo1.Run();

#region Demo1 - Assign integer

class Demo1 {
  public static void Assign(int target, int source) {
    target = source;
  }

  public static void Run() {
    var result = 0;
    for(int i = 0; i < 10; i++) {
      Assign(result, i);
    }
    // TODO: What does this print?
    // TODO: Add ref and/or out to Assign
    Console.WriteLine($"Result: {result}");
  }
}

#endregion
#region Demo2 - Assign point

record class Point {
  public int x;
  public int y;
}

class Demo2 {
  public static void Assign(Point target, Point source) {
    target = source;
  }

  public static void Rotate(Point point) {
    var oldY = point.y;
    point.y = point.x;
    point.x = oldY;
  }

  public static void Run() {
    Point p1 = new Point { x=100, y=200 };
    Point result = null;
    
    Assign(result, p1);

    // TODO: What happens here in this case?
    // TODO: Add ref/out (out avoids null!)
    // TODO: What if we call Rotate(result)?
    // TODO: Modify Assign to copy field values
    Console.WriteLine($"P1: {result}");
    Console.WriteLine($"Result: {result}");
  }
}

#endregion
#region Demo3 - Tricky assign

public class Demo3 {
  public static void Run() {
    Point p1 = new Point { x=1, y=2 };
    Foo(ref p1, p1);
  }

  static void Foo(ref Point p1, Point p2) {
    p1 = new Point { x=100, y=200 };
    p2.x = 42;
  }
}
#endregion