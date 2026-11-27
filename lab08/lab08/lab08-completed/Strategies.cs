using System;
using System.Collections.Generic;
using System.Text;

namespace StardewValley {
  interface IFruitProductionStrategy {
    IFruit Produce();
  }

  class SimpleProductionStrategy : IFruitProductionStrategy {
    public IFruitFractory Factory { get; init; }
    public IFruit Produce() {
      return Factory.Create();
    }
  }

  class SwitchingProductionStrategy : IFruitProductionStrategy {
    public IFruitFractory DefaultFactory { get; init; }
    public IFruitFractory SpecialFactory { get; init; }
    public int Rate { get; init; }
    private int count = 0;

    public IFruit Produce() {
      if (count++ % Rate == (Rate - 1)) 
        return SpecialFactory.Create();
      else 
        return DefaultFactory.Create();
    }
  }
}
