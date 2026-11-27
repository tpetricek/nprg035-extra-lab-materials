using System;
using System.Collections.Generic;
using System.Text;

namespace StardewValley {
  interface IFruit { }
  interface IEdible : IFruit {
    public void Eat();
  }
  interface IFruitFractory {
	  IFruit Create();
  }
}
