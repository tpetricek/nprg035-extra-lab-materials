namespace EnumAsContract {
  #region Better approach using enums
  /*
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
  */
  #endregion

  #region More ideas 
  // Limited int at runtime
  // Specifying underlying numerical representation
  #endregion

  #region Poor design using static attributes 

  public static class Month {
    public static int January { get; } = 1;
    public static int February { get; } = 2;
    public static int March { get; } = 3;
    public static int April { get; } = 4;
    public static int May { get; } = 5;
    public static int June { get; } = 6;
    public static int July { get; } = 7;
    public static int August { get; } = 8;
    public static int September { get; } = 9;
    public static int October { get; } = 10;
    public static int November { get; } = 11;
    public static int December { get; } = 12;
  }

  public static class DayOfWeek
  {
    public static int Monday { get; } = 1;
    public static int Tuesday { get; } = 2;
    public static int Wednesday { get; } = 3;
    public static int Thursday { get; } = 4;
    public static int Friday { get; } = 5;
    public static int Saturday { get; } = 6;
    public static int Sunday { get; } = 7;
  }

  #endregion

  class DatePrinter {
    public void PrintMonthSeason(int month) {
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

    public void PrintDayType(int dayOfWeek) {
      if (dayOfWeek >= 1 && dayOfWeek <= 5) {
          Console.WriteLine("Weekday");
      } else if (dayOfWeek == 6 || dayOfWeek == 7) {
          Console.WriteLine("Weekend");
      }
    }	
  }

	static class Dates {
		public static void Run() {
			DatePrinter dp = new DatePrinter();
      dp.PrintMonthSeason(Month.October);
      dp.PrintDayType(DayOfWeek.Friday);

      // BAD THINGS - should not be allowed
      dp.PrintMonthSeason(DayOfWeek.Friday);
      dp.PrintDayType(Month.October);
      dp.PrintDayType(Month.January + DayOfWeek.Tuesday);
		}
	}
}