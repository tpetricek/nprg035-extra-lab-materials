using System.Diagnostics;
using System.Linq;


long total = 0;
for(var iteration = 0; iteration < 20; iteration++) {
  var sw = Stopwatch.StartNew();
  for(var i = 0; i < 100; i++)
    Demo.Run(10000);
  Console.WriteLine(sw.ElapsedMilliseconds);
  total += sw.ElapsedMilliseconds;
}
Console.WriteLine("Average: {0}", total / 20);


public static class Demo {
  public static int Run (int length) {
    // Generate string of a given length containing a
    // sequence of dots and whitespace i.e. ". . . . . "
    // Then return the length of the string
    return -1;
  }
}