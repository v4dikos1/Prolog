using Prolog.Core.Loggers.CustomAttributes;
using System.Reflection;

namespace Prolog.Core.Loggers.Helpers;

public static class LoggablePropertyHelper
{
    /// <summary>
    /// Получение свойств объекта (в том числе и вложенных объектов), помеченных атрибутом [LoggableProperty]
    /// </summary>
    /// <param name="obj">объект</param>
    /// <returns></returns>
    public static List<LoggablePropertyInfo> ExtractLoggableProperties(object obj)
    {
        return ExtractProperties(obj, "");
    }

    private static List<LoggablePropertyInfo> ExtractProperties(object obj, string parentName = "")
    {
        var result = new List<LoggablePropertyInfo>();

        foreach (var property in obj.GetType().GetProperties())
        {
            var loggableAttribute = property.GetCustomAttribute<LoggablePropertyAttribute>();
            var value = property.GetValue(obj);
            var propertyName = string.IsNullOrEmpty(parentName) ? property.Name : $"{parentName}.{property.Name}";

            if (loggableAttribute != null)
            {
                var key = string.IsNullOrEmpty(loggableAttribute.FieldName) ? property.Name : loggableAttribute.FieldName;
                result.Add(new LoggablePropertyInfo(key, value));
            }
            else if (value != null && IsComplexObject(property.PropertyType))
            {
                result.AddRange(ExtractProperties(value, propertyName));
            }
        }

        return result;
    }

    private static bool IsComplexObject(Type type)
    {
        return !type.IsPrimitive && type != typeof(string) && !type.IsValueType && !type.IsEnum;
    }
}