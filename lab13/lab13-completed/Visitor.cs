using System;
using System.IO;

namespace Fruits;

interface IEdible {
	public void Accept(IEdibleVisitor edibleVisitor);
}

abstract class Apple : IEdible {
  abstract public int Energy { get; }
  abstract public void Accept(IEdibleVisitor edibleVisitor);
}

class RegularApple : Apple {
  public override int Energy => 5;
  public override void Accept(IEdibleVisitor edibleVisitor) {
    edibleVisitor.Visit(this);
  }
}

class GoldenApple : Apple {
  public override int Energy => 10;
  public override void Accept(IEdibleVisitor edibleVisitor) {
    edibleVisitor.Visit(this);
  }
}

interface IEdibleVisitor {
	public void Visit(RegularApple apple);
	public void Visit(GoldenApple goldenApple);
}

public class CountEnergyVisitor : IEdibleVisitor {
  public int Energy { get; private set; } = 0;

  void IEdibleVisitor.Visit(RegularApple apple) {
    Energy += apple.Energy;
  }

  void IEdibleVisitor.Visit(GoldenApple apple) {
    Energy += apple.Energy * 2;
  }
}

public class Demos {
  static int CountEnergy(List<IEdible> edibles) {
    int energy = 0;
    foreach(var edible in edibles) 
    {
      switch(edible) {
        case RegularApple ge:
          energy += ge.Energy; 
          break;

        case GoldenApple ge:
          energy += ge.Energy * 2; 
          break;
      }
    }
    return energy;
  }

  public static void Run() {
    List<IEdible> edibles = new List<IEdible>();
    edibles.Add(new RegularApple());
    edibles.Add(new GoldenApple()); 
    edibles.Add(new GoldenApple()); 

    Console.WriteLine(CountEnergy(edibles));

    var counter = new CountEnergyVisitor();
    foreach(var edible in edibles) edible.Accept(counter);
    Console.WriteLine(counter.Energy);
  }
}