using HomeBudget.WebApplication.Models;
using Xunit;

namespace HomeBudget.WebApplication.Tests
{
    public class RevenueViewModelTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(0.001)]
        [InlineData(-1)]
        public void RevenueViewModel_validation_should_return_invalid_when_price_smaller_than_one_tenth(double price)
        {
            var model = new RevenueViewModel()
            {
                Price = price
            };
            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Wartość w polu Cena musi być większa lub równa 0.01", modelValidation.GetErrorMessagesAsString());
        }
        [Theory]
        [InlineData(0.011)]
        [InlineData(2.311)]
        [InlineData(double.MaxValue)]
        public void RevenueViewModel_validation_should_return_invalid_when_price_have_more_than_two_decimal_places(double price)
        {
            var model = new RevenueViewModel()
            {
                Price = price
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Maksymalnie dwa miejsca po przecinku", modelValidation.GetErrorMessagesAsString());
        }
        [Theory]
        [InlineData(0.01)]
        [InlineData(4)]
        [InlineData(3000.25)]
        public void RevenueViewModel_validation_should_no_return_invalid_price_when_price_have_less_than_2_decimal_places_and_is_higher_than_one_tenth(double price)
        {
            var model = new RevenueViewModel()
            {
                Price = price
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.DoesNotContain("Maksymalnie dwa miejsca po przecinku", modelValidation.GetErrorMessagesAsString());
        }
    }
}
