namespace Prolog.Core.Loggers.Helpers;

public class LoggablePropertyInfo(string propertyName, object? propertyValue)
{
    public string PropertyName { get; set; } = propertyName;
    public object? PropertyValue { get; set; } = propertyValue;
}