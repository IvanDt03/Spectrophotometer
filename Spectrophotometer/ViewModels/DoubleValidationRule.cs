using System.Globalization;
using System.Windows.Controls;

namespace Spectrophotometer.ViewModels;

public class DoubleValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value == null)
            return new ValidationResult(false, "Введите число");

        string input = value.ToString();

        if (string.IsNullOrWhiteSpace(input))
            return new ValidationResult(false, "Введите число");

        if (!double.TryParse(input, NumberStyles.Any, cultureInfo, out _))
            return new ValidationResult(false, "Некорректное число");

        return ValidationResult.ValidResult;
    }
}
