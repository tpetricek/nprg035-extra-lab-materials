using System;
using System.Collections.Generic;
using System.Text;

namespace StardewValley {
  class PeachFactory : IFruitFractory {
	  public virtual IFruit Create() {
		  return new Peach();
    }
  }
  class FrnakovnikFactory : IFruitFractory {
    public int DefaultEnergy { get; init; }
    public virtual IFruit Create() {
		  return new NoseGrowingApple() { Energy = DefaultEnergy };
    }
  }
  class AppleFactory : IFruitFractory {
    public bool MakesGoldenApples { get; init; }
    public virtual IFruit Create() {
		  if (MakesGoldenApples) return new GoldenApple { Energy = 10 };
		  else return new Apple { Energy = 10 };
    }
  }

}
