using System.Reflection.Metadata;

namespace TableSummator;

interface ITableSummatorState {
	abstract bool ProcessingColumnHeaders { get; set; }
	abstract int HeaderColumnCount { get; set; }
	abstract int CurrentColumn { get; set; }

	abstract bool FoundTargetColumn { get; set; }
	abstract int TargetColumnIndex { get; set; }
	abstract long Sum { get; set; }
}

public class TableSummatorState : ITableSummatorState {
	public bool ProcessingColumnHeaders { get; set; } = true;
	public int HeaderColumnCount { get; set; }
	public int CurrentColumn { get; set; } = 0;

	public bool FoundTargetColumn { get; set; } = false;
	public int TargetColumnIndex { get; set; }
	public long Sum { get; set; } = 0;
}

public record class TableSummatorProcessorState(TextWriter OutputWriter, string TargetColumnName, ITableSummatorState _state) : ITokenProcessor {
	private ITableSummatorState _state = _state;

  public void ProcessToken(Token token) {
		if (_state.ProcessingColumnHeaders) {
			ProcessHeaderToken(token);
		} else {
			ProcessTableDataToken(token);
		}
	}

	private void ProcessHeaderToken(Token token) {
		switch (token.Type) {
			case TokenType.Word:
				if (!_state.FoundTargetColumn && StringComparer.CurrentCultureIgnoreCase.Compare(token.Value, TargetColumnName) == 0) {
					_state.TargetColumnIndex = _state.CurrentColumn;
					_state.FoundTargetColumn = true;
				}
				_state.CurrentColumn++;
				break;
			case TokenType.EndOfLine:
				if (_state.CurrentColumn == 0) {
					throw new Exception("Invalid file format");
				} else if (!_state.FoundTargetColumn) {
					throw new Exception("Non-existent column");
				}
				_state.HeaderColumnCount = _state.CurrentColumn;
				_state.CurrentColumn = 0;
				_state.ProcessingColumnHeaders = false;
				break;
			default:
				throw new Exception("Invalid file format");
		}
	}

	private void ProcessTableDataToken(Token token) {
		switch (token.Type) {
			case TokenType.Word:
				if (_state.CurrentColumn == _state.TargetColumnIndex) {
					if (int.TryParse(token.Value!, out int value)) {
						_state.Sum += value;
					} else {
						throw new Exception("Not a number");
					}
				}
				_state.CurrentColumn++;
				break;
			case TokenType.EndOfLine:
				if (_state.CurrentColumn == 0 || _state.CurrentColumn != _state.HeaderColumnCount) {
					throw new Exception("Invalid file fromat");
				}
				_state.CurrentColumn = 0;
				break;
			default:
				throw new Exception("Invalid file fomrat");
		}
	}

	public void Finish() {
		if (_state.ProcessingColumnHeaders) {
			throw new Exception("Invalid file format");
		}
		OutputWriter.WriteLine(TargetColumnName);
		OutputWriter.WriteLine(new string('-', TargetColumnName.Length));
		OutputWriter.WriteLine(_state.Sum);
	}
}
