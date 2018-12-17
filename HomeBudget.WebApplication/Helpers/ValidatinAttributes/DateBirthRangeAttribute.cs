using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Helpers.ValidatinAttributes
{
    public class DateBirthRangeAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            value = (DateTime)value;
            var dateTimeMin = new DateTime(1900, 01, 01);
            var dateTimeMax = DateTime.Now;
            if (dateTimeMin.CompareTo(value) <= 0 && dateTimeMax.CompareTo(value) >= 0)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(string.Format("{0} musi być pomiędzy {1} a {2}", validationContext.DisplayName, dateTimeMin.ToString("dd.MM.yyyy"), dateTimeMax.ToString("dd.MM.yyyy")));
        }
    }
}