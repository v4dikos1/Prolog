using Prolog.Core.TimeZones.Features.TimeZoneConverter;
using System.Text.Json.Serialization;

namespace Prolog.Core.TimeZones.Attributes;

/// <summary>
/// Атрибут для кастомного преобразования значений DateTimeOffset из Json и query-параметров.
/// Следует использовать, когда клиент может присылать дату как со смещением (например, +07:00 или -05:30), так и без нее, и необходимо избавиться
/// от стандартного поведения установки локального смещения сервера в DateTimeOffset (если при парсинге строки не передать значение смещения,
/// то по умолчанию возьмется смещение сервера, т.к offset обязателен в DateTimeOffset).
/// Для сереаилизации/десереаилизации из JSON требуется JsonConverter, для работы с query-параметрами требуется modelBinder.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class DefaultTimeZoneOffsetAttribute : JsonConverterAttribute
{
    public TimeSpan DefaultDatetimeOffset { get; }

    /// <summary>
    /// Атрибут для кастомного преобразования значений DateTimeOffset из Json и query-параметров.
    /// </summary>
    /// <param name="offset">Строка смещения в формате: (-)hh:mm"</param>
    public DefaultTimeZoneOffsetAttribute(string offset = "0:00")
    {
        if (!TimeSpan.TryParse(offset, out var parsedOffset))
        {
            throw new ArgumentException("Неверный формат смещения. Смещение должно следовать следующему формату: '(-)hh:mm'.");
        }

        DefaultDatetimeOffset = parsedOffset;
    }

    public override JsonConverter CreateConverter(Type typeToConvert)
    {
        return new DefaultTimeZoneConverter(DefaultDatetimeOffset);
    }
}