using System.Globalization;
using System.Windows.Controls;

namespace StudentManagementWPF.Validations
{
    public class CourseHoursValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if(!int.TryParse(value.ToString(), out var hours) || hours <= 0)
            {
                return new ValidationResult(false, "Course hours must be a non-negative number");
            }
            return ValidationResult.ValidResult;
        }
    }
}
