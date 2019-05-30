using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeBudget.WebApplication.Tests
{
    public class ValidationModel
    {
        private ValidationContext _context;
        private List<ValidationResult> _result;
        private bool _valid;
        private object _model;
        public ValidationModel(object model)
        {
            _model = model;
            _context = new ValidationContext(_model, null, null);
            _result = new List<ValidationResult>();
            _valid = Validator.TryValidateObject(_model, _context, _result, true);
        }
        public bool IsValid()
        {
            return _valid;
        }
        public List<string> GetErrorMessages()
        {
            return _result.Select(x => x.ErrorMessage).ToList();
        }
        public string GetErrorMessagesAsString()
        {
            return String.Join(", ", _result.Select(x => x.ErrorMessage).ToArray());
        }
    }
}
