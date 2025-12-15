Demo3.Run();

#region Demo1 - Assign integer

class Demo1 {
  public static void Assign(int target, int source) {
    target = source;
  }

  public static void Run() {
    int result;
    Assign(result, 101);
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
    // TODO
  }

  public static void Rotate(Point point) {
    var oldY = point.y;
    point.y = point.x;
    point.x = oldY;
  }

  public static void Run() {
    Point p1 = new Point { x=100, y=200 };
    Point result = new Point { x=0, y=0 };
    
    // TODO: Assing, Rotate

    // TODO: What happens here in this case?
    Console.WriteLine($"P1: {p1.x},{p1.y}");
    Console.WriteLine($"Result: {result.x},{result.y}");
  }
}

#endregion
#region Demo3 - Tricky assign

public class Demo3 {
  public static void Run() {
    Point p1 = new Point { x=1, y=2 };
    Foo(ref p1, ref p1);
    Console.WriteLine($"P1={p1.x}, {p1.y}");
  }

  static void Foo(ref Point p1, ref Point p2) {
    p1 = new Point { x=100, y=200 };
    p2.x = 42;
  }
}
#endregion