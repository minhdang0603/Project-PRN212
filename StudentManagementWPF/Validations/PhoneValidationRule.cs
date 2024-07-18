using BussinessObject;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace StudentManagementWPF.Validations
{
    public class PhoneValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(value == null || !Regex.IsMatch(value.ToString(), @"^0\d{9}$"))
            {
                return new ValidationResult(false, "Phone must start with 0 followed by 9 digits");
            }
            return ValidationResult.ValidResult;
        }
    }
}
