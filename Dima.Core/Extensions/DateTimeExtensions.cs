namespace Dima.Core.Extensions;

public static class DateTimeExtensions
{
    public static DateTime FirstDayInMonth(this DateTime date) =>
        new DateTime(date.Year, date.Month, 1);

    public static DateTime LastDayInMonth(this DateTime date) =>
        new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
}
