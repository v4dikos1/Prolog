namespace Prolog.Core.TimeZones.Extensions;

public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// Перевод даты в указанную часовую зону.
    /// </summary>
    /// <param name="dateTimeOffset">Дата</param>
    public static DateTimeOffset ToTimeZone(this DateTimeOffset dateTimeOffset, TimeZoneInfo timeZone)
    {
        return TimeZoneInfo.ConvertTime(dateTimeOffset, timeZone);
    }
}