using System.Globalization;
using System.Windows.Controls;

namespace Spectrophotometer.ViewModels;

public class DoubleValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        if (value == null)
            return new ValidationResult(false, "Введитите число");

        var input = (value as string)?.Replace(',', '.');

        if (string.IsNullOrEmpty(input))
            return new ValidationResult(false, "Введите число");

        if (double.TryParse(input, CultureInfo.InvariantCulture, out double result))
        {
            if (result < 0)
                return new ValidationResult(false, "Значение не может быть отрицательным");
            return ValidationResult.ValidResult;
        }
           

        return new ValidationResult(false, "Некорректное значение. Введите число, например, 0.7 или 0,7");
    }
}
