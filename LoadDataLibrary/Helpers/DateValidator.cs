using System.Globalization;
using LoadDataLibrary.Interfaces;

namespace LoadDataLibrary.Helpers;

public class DateValidator : IDateValidator
{
    public bool IsValidDate(string value, string[] formats)
    {
        return DateTime.TryParseExact(value.Trim(), formats, CultureInfo.InvariantCulture,  DateTimeStyles.None, out DateTime _);
    }
}