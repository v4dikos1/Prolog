using System.Text.Json;
using System.Text.Json.Serialization;

namespace Prolog.Core.TimeZones.Features.TimeZoneConverter;

public class DefaultTimeZoneConverter : JsonConverter<DateTimeOffset?>
{
    private readonly TimeSpan _defaultOffset;

    /// <summary>
    /// Конвертер UTC.
    /// Используется для преобразования строки в DateTimeOffset, когда неизвестно, будет ли указано смещение (offset),
    /// и во избежание стандартного поведения установки offset в значение локального смещения на сервере.
    /// Если смещение указано, возврщается дата с исходным смещением,
    /// Если смещение не указано, возврщается дата со смещением defaultOffset.
    /// </summary>
    /// <param name="defaultOffset">Смещение по умолчанию, используемое если в исходной строке смещение не указано.</param>
    public DefaultTimeZoneConverter(TimeSpan defaultOffset)
    {
        _defaultOffset = defaultOffset;
    }

    public override DateTimeOffset? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTimeStr = reader.GetString();
        if (string.IsNullOrEmpty(dateTimeStr))
        {
            return null;
        }
        return ConvertFromString(dateTimeStr);
    }

    public DateTimeOffset ConvertFromString(string dateTimeStr)
    {
        if (string.IsNullOrEmpty(dateTimeStr))
        {
            throw new ArgumentNullException(nameof(dateTimeStr), "Строка даты не должна быть пустой!");
        }
        // Попытка спарсить строку к DateTime для проверки свойства Kind. Если Kind = Unspecified, значит в исходной строке не было задано смещение.
        // Используется именно DateTime вместо DateTimeOffset с целью избежать стандарнтное поведение DateTimeOffset с установкой смещения в локальное смещение сервера,
        // если смещение не задано.
        if (DateTime.TryParse(dateTimeStr, out var parsedDateTime) && parsedDateTime.Kind == DateTimeKind.Unspecified)
        {
            // Если смещение не задано, используется смещение по умолчанию.
            return new DateTimeOffset(parsedDateTime, _defaultOffset);
        }
        // Если смещение задано и строка парсится в DateTimeOffset, используется исходное смещение.
        if (DateTimeOffset.TryParse(dateTimeStr, out var parsedDateTimeOffset))
        {
            return parsedDateTimeOffset;
        }
        throw new ApplicationException($"Не удалось преобразовать дату и время из строки \"{dateTimeStr}\".");
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset? value, JsonSerializerOptions options)
    {
        // Стандартное поведение
        JsonSerializer.Serialize(writer, value, options);
    }
}