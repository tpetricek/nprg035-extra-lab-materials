namespace EnumAsContract {
  public enum Month
  {
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
  }

  public enum DayOfWeek
  {
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7
  }
	class DatePrinter {
    public void PrintMonthSeason(Month monthToPrint) {
      int month = (int)monthToPrint;
      if (month == 12 || month == 1 || month == 2) {
          Console.WriteLine("Winter");
      } else if (month >= 3 && month <= 5) {
          Console.WriteLine("Spring");
      } else if (month >= 6 && month <= 8) {
          Console.WriteLine("Summer");
      } else if (month >= 9 && month <= 11) {
          Console.WriteLine("Autumn");
      }
    }

    public void PrintDayType(DayOfWeek dayOfWeekToPrint) {
      int dayOfWeek = (int)dayOfWeekToPrint;
      if (dayOfWeek >= 1 && dayOfWeek <= 5) {
          Console.WriteLine("Weekday");
      } else if (dayOfWeek == 6 || dayOfWeek == 7) {
          Console.WriteLine("Weekend");
      }
    }	
  }

	internal class Program {
		static void Main(string[] args) {
			Month month = Month.January;
			DatePrinter dp = new DatePrinter();
      dp.PrintMonthSeason(month);
      //dp.PrintDayType(month);
      Console.WriteLine(month.ToString());
		}
	}
}