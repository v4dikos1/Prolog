namespace Prolog.Core.Loggers.CustomAttributes;

/// <summary>
/// Атрибут, использующийся для логирования значения свойства после выполения запроса.
/// <param name="fieldName">Имя поля (по умолчанию совпадает с именем свойства)</param>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class LoggablePropertyAttribute(string? fieldName = null) : Attribute
{
    public string? FieldName { get; private set; } = fieldName;
}