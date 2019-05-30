using HomeBudget.WebApplication.Models;
using System;
using Xunit;
namespace HomeBudget.WebApplication.Tests
{
    public class FinanceViewModelTests
    {
        [Fact]
        public void FinanceViewModel_validation_should_return_invalid_when_all_properties_empty()
        {
            var model = new FinanceViewModel();
          
            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.True(modelValidation.GetErrorMessages().Contains("Pole Nazwa jest wymagane."));
        }
        [Fact]
        public void FinanceViewModel_validation_should_return_invalid_when_name_empty()
        {
            var model = new FinanceViewModel();

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Nazwa jest wymagane.", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void FinanceViewModel_validation_should_return_invalid_when_time_event_lower_than_1900()
        {
            var model = new FinanceViewModel()
            {
                TimeEvent = new DateTime(1899, 12, 31)
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Data przychodu musi być pomiędzy 01.01.1900", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void FinanceViewModel_validation_should_return_valid_model_when_name_is_not_null_and_timeEvent_is_higher_than_1900()
        {
            var model = new FinanceViewModel()
            {
                Name = "Finance",
                TimeEvent = new DateTime(1900, 01, 01)
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.True(valid);
            Assert.True(modelValidation.GetErrorMessages().Count == 0);
        }
    }
}
