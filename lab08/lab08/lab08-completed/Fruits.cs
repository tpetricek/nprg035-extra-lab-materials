using System;
using System.Collections.Generic;
using System.Text;

namespace StardewValley {
  class Peach : IFruit, IEdible {
    public int Energy { get { return 7; } }
    public virtual void Eat() {
      Console.WriteLine($"Peach eaten, player got {Energy} units of energy.");
    }
  }
  class Apple : IFruit, IEdible {
    public int Energy { get; init; }
    public virtual void Eat() {
      Console.WriteLine($"Apple eaten, player got {Energy} units of energy.");
    }
  }
  class GoldenApple : Apple {
    public override void Eat() {
		  base.Eat();
      Console.WriteLine("Golden apple increased player's luck.");
    }
  }
  class NoseGrowingApple : Apple {
    public override void Eat() {
		  base.Eat();
      Console.WriteLine("Player's nose grew 100x!");
    }
  }
}
