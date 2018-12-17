using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HomeBudget.WebApplication.Helpers.ValidatinAttributes
{
    public class MaxValueAttribute : ValidationAttribute
    {
        private readonly double _maxValue;

        public MaxValueAttribute(double maxValue)
        {
            _maxValue = maxValue;
        }

        public override bool IsValid(object value)
        {
            return (double)value <= _maxValue;
        }
    }

}