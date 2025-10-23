namespace Demo2 { 
	static class Demo2 {
		static public void Run() { 
			I2 i2a = new D1();
			i2a.f();
			i2a.g();

			I2 i2b = new D2();
			i2b.f();
			i2b.g();

			I2 i2c = new D3();
			i2c.f();
			i2c.g();
		}
	}

	interface I2 {
		public void f();
		public void g();
	}

	class D1 : I2 {
		public virtual void f() {
			Console.WriteLine("D1.f");
		}

		public virtual void g() {
			Console.WriteLine("D1.g");
		}
	}

	class D2 : D1 {
		public override void g() {
			Console.WriteLine("D2.g");
		}
	}

	class D3 : D1, I2 {
		public override void g() {
			Console.WriteLine("D3.g");
		}
	}
}
