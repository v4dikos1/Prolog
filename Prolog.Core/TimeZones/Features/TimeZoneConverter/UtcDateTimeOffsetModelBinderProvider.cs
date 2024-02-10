using Microsoft.AspNetCore.Mvc.ModelBinding;
using Prolog.Core.TimeZones.Attributes;

namespace Prolog.Core.TimeZones.Features.TimeZoneConverter;

/// <summary>
/// Провайдер для привязчика модели UtcDateTimeOffsetModelBinder
/// Пример использования:
/// builder.Services.AddControllers(o =>
/// {
///     o.ModelBinderProviders.Insert(0, new UtcDateTimeOffsetModelBinderProvider());
/// });
/// </summary>
public class UtcDateTimeOffsetModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(DateTimeOffset) || context.Metadata.ModelType == typeof(DateTimeOffset?))
        {
            var hasAttribute = context.Metadata.ContainerType?.GetProperty(context.Metadata.PropertyName)
                ?.GetCustomAttributes(typeof(DefaultTimeZoneOffsetAttribute), false)
                .Any() == true;

            if (hasAttribute)
            {
                return new UtcDateTimeOffsetModelBinder();
            }
        }

        return null;
    }
}