using System.Diagnostics;
using System.Linq;


long total = 0;
for(var iteration = 0; iteration < 20; iteration++) {
  var sw = Stopwatch.StartNew();
  for(var i = 0; i < 100; i++)
    Demo.Run();
  Console.WriteLine(sw.ElapsedMilliseconds);
  total += sw.ElapsedMilliseconds;
}
Console.WriteLine("Average: {0}", total / 20);


public static class Demo {
  public static int Run () {
    var s = "";
    for(var i = 0; i < 10000; i++) {
      s += i % 2 == 0 ? "." : " ";
    }
    return s.Count();
  }
}