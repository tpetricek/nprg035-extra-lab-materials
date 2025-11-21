using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableSummator;
using Xunit;

namespace lab07_tablesummator {
  public class TableSummatorStateForTest : ITableSummatorState {
    public List<long> SumCalls = new List<long>();

	  public bool ProcessingColumnHeaders { get; set; } = true;
	  public int HeaderColumnCount { get; set; }
	  public int CurrentColumn { get; set; } = 0;

	  public bool FoundTargetColumn { get; set; } = false;
	  public int TargetColumnIndex { get; set; }
	  
    private long _sum = 0;
    public long Sum { get { return _sum; } set { SumCalls.Add(value); _sum = value; } }
  }

  public class TableSummatorTests {
    private void ProcessTokens(ITokenProcessor proc, Token[] tokens) {
      foreach(var token in tokens) proc.ProcessToken(token);
    }

    [Fact]
    public void OriginalTest() {
      TableSummatorProcessor tsp = new TableSummatorProcessor(Console.Out, "a");
      ProcessTokens(tsp, new[] { new Token("a"), new Token(TokenType.EndOfLine) });
      Assert.Equal(0, tsp.TargetColumnIndex);
      Assert.True(tsp.FoundTargetColumn);
    }

    [Fact]
    public void PublicTest() {
      TableSummatorProcessorPublic tsp = new TableSummatorProcessorPublic(Console.Out, "a");
      Assert.True(tsp.ProcessingColumnHeaders);
      ProcessTokens(tsp, new[] { new Token("a"), new Token(TokenType.EndOfLine) });
      Assert.False(tsp.ProcessingColumnHeaders);
      ProcessTokens(tsp, new[] { new Token("123"), new Token(TokenType.EndOfLine) });
      Assert.Equal(123, tsp.Sum);
    }

    [Fact]
    public void StateTest() {
      TableSummatorStateForTest tspState = new TableSummatorStateForTest();
      TableSummatorProcessorState tsp = new TableSummatorProcessorState(Console.Out, "a", tspState);
      ProcessTokens(tsp, new[] { new Token("a"), new Token(TokenType.EndOfLine) });
      ProcessTokens(tsp, new[] { new Token("123"), new Token(TokenType.EndOfLine) });
      ProcessTokens(tsp, new[] { new Token("456"), new Token(TokenType.EndOfLine) });
      ProcessTokens(tsp, new[] { new Token("1"), new Token(TokenType.EndOfLine) });
      Assert.Equal(new long[] { 123, 579, 580 }, tspState.SumCalls.ToArray());
    }

    [Fact]
    public void SplitHeaderTest() {
      TableSummatorHeaderProcessor tsp = new TableSummatorHeaderProcessor("a");
      ProcessTokens(tsp, new[] { new Token("a"), new Token(TokenType.EndOfLine) });
      Assert.Equal(1, tsp.HeaderColumnCount);
    }

    [Fact]
    public void SplitBodyTest() {
      TableSummatorBodyProcessor tsp = new TableSummatorBodyProcessor(Console.Out, "a", 0, 1);
      ProcessTokens(tsp, new[] { new Token("123"), new Token(TokenType.EndOfLine) });
      Assert.Equal(123, tsp.Sum);
    }
  }
}
