using System;
using System.IO;

namespace Fruits;

interface IEdible {
}

abstract class Apple : IEdible {
  abstract public int Energy { get; }
}

class RegularApple : Apple {
  public override int Energy => 5;
}

class GoldenApple : Apple {
  public override int Energy => 10;
}

public class Demos {
  static int CountEnergy(List<IEdible> edibles) {
    int energy = 0;
    foreach(var edible in edibles) 
    {
      // Add Energy for regular apple
      // Add 2*Energy for golden apple
    }
    return energy;
  }

  public static void Run() {
    List<IEdible> edibles = new List<IEdible>();
    edibles.Add(new RegularApple());
    edibles.Add(new GoldenApple()); 
    edibles.Add(new GoldenApple()); 
    Console.WriteLine(CountEnergy(edibles));
  }
}