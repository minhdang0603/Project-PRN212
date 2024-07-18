using System.Globalization;
using System.Windows.Controls;

namespace StudentManagementWPF.Validations
{
    public class ScoreValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!decimal.TryParse(value.ToString(), out var score) || score < 0)
            {
                return new ValidationResult(false, "Score must be a non-negative decimal number");
            }
            return ValidationResult.ValidResult;
        }
    }
}
