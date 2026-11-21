namespace Demo1 { 
	static class Demo1 {
		static public void Run() { 
			D3 d1a = new D3();
			d1a.f();
			d1a.g();

			D1 d1b = new D3();
			d1b.f();
			d1b.g();
			
			D1 d1c = new D3();
			d1c.f();
			d1c.g();
		}
	}

	class D1 {
		public void f() {
			Console.WriteLine("D1.f");
		}

		public virtual void g() {
			Console.WriteLine("D1.g");
		}
	}

	class D2 : D1 {
		public virtual new void g() {
			Console.WriteLine("D2.g");
		}
	}

	class D3 : D2 {
		public override void g() {
			Console.WriteLine("D3.g");
		}
	}
}
