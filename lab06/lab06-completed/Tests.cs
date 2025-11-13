using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Lab06 {
  public class SimpleWordCounterTests {
    // TODO: SimpleWordCounter tests!

    [Fact]
    public void CountsEmptyInputAsZero() {      
      SimpleWordCounter wc = new SimpleWordCounter();
      Assert.Equal(0, wc.Count);
    }

    [Fact]
    public void CountsTwoWordInputs() {      
      SimpleWordCounter wc = new SimpleWordCounter();
      wc.ProcessWord("hi");
      wc.ProcessWord("there");
      Assert.Equal(2, wc.Count);
    }
  }
}
