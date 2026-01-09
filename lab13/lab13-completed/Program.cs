using System;

// Run expressions demos
Expressions.Demos.Run();

// Run arithmetic demos
Arithmetic.Run();

// Run fruit demos
Fruits.Demos.Run();

public class Arithmetic { 
  public static void Run() { 

    int i1 = Int32.MaxValue;
    Console.WriteLine("max         = {0}", i1);
    Console.WriteLine("max+max     = {0}", i1 + i1);
    Console.WriteLine("max+1       = {0}", i1 + 1);
    Console.WriteLine("max (b)     = {0}", Convert.ToString(i1, 2));
    Console.WriteLine("max+max (b) = {0}", Convert.ToString(i1 + i1, 2));
    Console.WriteLine("max+1 (b)   = {0}", Convert.ToString(i1 + 1, 2));
  }
}