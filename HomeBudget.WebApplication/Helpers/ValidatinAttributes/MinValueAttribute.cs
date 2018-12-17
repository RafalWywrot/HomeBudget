using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Helpers.ValidatinAttributes
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly double _minValue;
        public MinValueAttribute(double minValue)
        {
            _minValue = minValue;
        }

        public override bool IsValid(object value)
        {
            return (double)value >= _minValue;
        }
    }

}