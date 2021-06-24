using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.Enums
{
    public class RuleTypeDatas
    {
        public static string Single => "Single";
        public static string Aggregated => "Aggregated";
    }

    public class DataCoverageSettingDatas
    {
        public static string Daily => "Daily";
        public static string Monthly => "Monthly";
    }

    public class ValueTypeDatas
    {
        public static string Amount => "Amount";
        public static string Percentage => "Percentage";
    }

    public class WeeklyDayDatas
    {
        public static string Sunday => "Sunday";
        public static string Monday => "Monday";
        public static string Tuesday => "Tuesday";
        public static string Wednesday => "Wednesday";
        public static string Thursday => "Thursday";
        public static string Friday => "Friday";
        public static string Saturday => "Saturday";
    }

    public class MonthDatas
    {
        public static string Monthly => "Monthly";
        public static string WithInterval => "With Interval";
    }

    public class MonthDayDatas
    {
        public static string AllDays => "All Days";
        public static string FifteenLast => "15, Last";
        public static string Last => "Last";
    }

    public class DetailDatas
    {
        public static string CurrentDay => "Current Day";
        public static string LastXNumberofTransactionDays => "Last X Number of Transaction Days";
        public static string SemiMonthly => "Semi-Monthly";
        public static string Monthly => "Monthly";
        public static string LastXNumberofTransactionMonths => "Last X Number of Transaction Months";
    }
}
