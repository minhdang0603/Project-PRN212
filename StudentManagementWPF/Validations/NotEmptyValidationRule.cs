using System.Globalization;
using System.Windows.Controls;

namespace StudentManagementWPF.Validations
{
    public class NotEmptyValidationRule : ValidationRule
    {
        public string? ErrorMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, ErrorMessage);
            }
            return ValidationResult.ValidResult;
        }
    }
}
