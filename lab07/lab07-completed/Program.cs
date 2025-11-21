using static System.Runtime.InteropServices.JavaScript.JSType;

Part1.Run();
//Part2.Run();

#region Part 1 - Virtual methods and interfaces referesher

class Part1 {
	public static void Run() {
		B b = new B();
		b.m1();
		b.m2();
		Console.WriteLine();
		I1 i1 = b;
		i1.m1();	// SHOULD CALL B.m1()
		i1.m2();	// SHOULD CALL A.m2()
	}
}


interface I1 {
	public void m1();
	public void m2();
}

class A : I1 {
	public void m1() {
    Console.WriteLine("A.m1()");
  }

	public virtual void m2() {
		Console.WriteLine("A.m2()");
	}
}

class B : A {
	public new void m1() {
		Console.WriteLine("B.m1()");
	}

	public override void m2() {
		Console.WriteLine("B.m2()");
	}
}

#endregion
#region Part 2 - Indexers

class Part2 {
	public static void Run() {
		MyNumber n = new MyNumber();
		n[5] = 5;
		Console.WriteLine(n.Number);
	}
}

public struct MyNumber {
	private int _number;
	public int Number { get { return _number; } }
	public int this[int n] {
		get { return (_number / (int)Math.Pow(10, n - 1)) % 10; }
		set {			
			int pow10 = (int)Math.Pow(10, n - 1);
			int currentDigit = (_number / pow10) % 10;
			_number = _number - currentDigit * pow10 + value * pow10;
		}
	}
}


#endregion