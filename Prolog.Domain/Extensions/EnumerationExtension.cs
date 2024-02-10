namespace Prolog.Domain.Extensions;

public static class EnumerationExtension
{
    public static Dictionary<int, string> GetDictionaryByEnum(Type typeEnum)
    {
        var array = Enum.GetValues(typeEnum);
        var result = array
            .Cast<object>()
            .ToDictionary(
                key => (int)key,
                value => ((Enum)value).GetDescription());
        return result;
    }

    public static string GetDescription(this Enum value)
    {
        // get attributes  
        var field = value?.GetType().GetField(value.ToString());
        if (field != null)
        {
            var attributes = field.GetCustomAttributes(false);

            // Description is in a hidden Attribute class called DisplayAttribute
            // Not to be confused with DisplayNameAttribute
            dynamic displayAttribute = null;

            if (attributes.Any())
            {
                displayAttribute = attributes.ElementAt(0);
            }

            // return description
            return displayAttribute?.Description ?? "Description Not Found";
        }

        return "Description Not Found";
    }
}