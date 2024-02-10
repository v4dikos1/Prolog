using Microsoft.AspNetCore.Mvc.ModelBinding;
using Prolog.Core.TimeZones.Attributes;
using System.Reflection;

namespace Prolog.Core.TimeZones.Features.TimeZoneConverter;

/// <summary>
/// Привязчик модели для свойств с типом DateTimeOffset и атрибутом [DefaultTimeZoneOffsetAttribute].
/// Используется для свойств, помеченных атрибутом [FromQuery].
/// </summary>
public class UtcDateTimeOffsetModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var propertyName = bindingContext.ModelMetadata.PropertyName;
        var property = bindingContext.ModelMetadata.ContainerType.GetProperty(propertyName) ?? throw new InvalidOperationException();
        var attribute = property.GetCustomAttribute<DefaultTimeZoneOffsetAttribute>();

        if (attribute != null)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (valueProviderResult != ValueProviderResult.None && !string.IsNullOrEmpty(valueProviderResult.FirstValue))
            {
                var converter = new DefaultTimeZoneConverter(attribute.DefaultDatetimeOffset);
                var dateTimeStr = valueProviderResult.FirstValue;

                try
                {
                    var dateTimeOffset = converter.ConvertFromString(dateTimeStr);
                    bindingContext.Result = ModelBindingResult.Success(dateTimeOffset);
                }
                catch (Exception ex)
                {
                    bindingContext.ModelState.TryAddModelError(propertyName, ex.Message);
                }
            }
        }

        return Task.CompletedTask;
    }
}