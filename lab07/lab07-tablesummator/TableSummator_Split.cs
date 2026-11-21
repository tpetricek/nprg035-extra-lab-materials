namespace TableSummator;

public record class TableSummatorHeaderProcessor(string TargetColumnName) : ITokenProcessor { 
	private int _currentColumn = 0;
	private bool _foundTargetColumn = false;

	public int TargetColumnIndex { get; private set; }
	public int HeaderColumnCount { get; private set; }
	public bool Completed { get; private set; } = false;

  public void Finish() {
    throw new Exception("Invalid file format");
  }

  public void ProcessToken(Token token) {
		switch (token.Type) {
			case TokenType.Word:
				if (!_foundTargetColumn && StringComparer.CurrentCultureIgnoreCase.Compare(token.Value, TargetColumnName) == 0) {
					TargetColumnIndex = _currentColumn;
					_foundTargetColumn = true;
				}
				_currentColumn++;
				break;
			case TokenType.EndOfLine:
				if (_currentColumn == 0) {
					throw new Exception("Invalid file format");
				} else if (!_foundTargetColumn) {
					throw new Exception("Non-existent column");
				}
				HeaderColumnCount = _currentColumn;
				_currentColumn = 0;
				Completed = true;
				break;
			default:
				throw new Exception("Invalid file format");
		}
	}
}

public record class TableSummatorBodyProcessor(TextWriter OutputWriter, string TargetColumnName, int TargetColumnIndex, int HeaderColumnCount) : ITokenProcessor { 
	private int _currentColumn = 0;
  
	public long Sum { get; private set; }

  public void Finish() {
		OutputWriter.WriteLine(TargetColumnName);
		OutputWriter.WriteLine(new string('-', TargetColumnName.Length));
		OutputWriter.WriteLine(Sum);
  }

	public void ProcessToken(Token token) {
		switch (token.Type) {
			case TokenType.Word:
				if (_currentColumn == TargetColumnIndex) {
					if (int.TryParse(token.Value!, out int value)) {
						Sum += value;
					} else {
						throw new Exception("Not a number");
					}
				}
				_currentColumn++;
				break;
			case TokenType.EndOfLine:
				if (_currentColumn == 0 || _currentColumn != HeaderColumnCount) {
					throw new Exception("Invalid file fromat");
				}
				_currentColumn = 0;
				break;
			default:
				throw new Exception("Invalid file fomrat");
		}
	}

}

public class TableSummatorProcessorSplit : ITokenProcessor {
	TableSummatorHeaderProcessor? headerProcessor; 
	ITokenProcessor currentProcessor;
	TextWriter outputWriter;
	string targetColumnName;

	public TableSummatorProcessorSplit(TextWriter outputWriter, string targetColumnName) {
		headerProcessor = new TableSummatorHeaderProcessor(targetColumnName);
		currentProcessor = headerProcessor;
		this.targetColumnName = targetColumnName;
		this.outputWriter = outputWriter;
	}

  public void ProcessToken(Token token) {
		currentProcessor.ProcessToken(token);
		if (headerProcessor != null && headerProcessor.Completed) {
			currentProcessor = new TableSummatorBodyProcessor(outputWriter, targetColumnName, 
				headerProcessor.TargetColumnIndex, headerProcessor.HeaderColumnCount);
			headerProcessor = null;
		}
	}

	public void Finish() {
		currentProcessor.Finish();
	}
}
