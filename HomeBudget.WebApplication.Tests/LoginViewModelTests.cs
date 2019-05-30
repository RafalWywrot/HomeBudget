using HomeBudget.WebApplication.Models;
using Xunit;

namespace HomeBudget.WebApplication.Tests
{
    public class LoginViewModelTests
    {
        [Fact]
        public void LoginViewModel_validation_should_return_invalid_when_all_properties_are_empty()
        {
            var model = new LoginViewModel();
            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
        }
        [Fact]
        public void LoginViewModel_validation_should_return_invalid_when_email_empty()
        {
            var model = new LoginViewModel()
            {
                Email = string.Empty
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Email jest wymagane", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void LoginViewModel_validation_should_return_invalid_when_password_empty()
        {
            var model = new LoginViewModel()
            {
                Password = string.Empty
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Hasło jest wymagane", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void LoginViewModel_validation_should_return_valid_when_data_is_valid()
        {
            var model = new LoginViewModel()
            {
                Email = "ao2@on.pl",
                Password = "a"
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.True(valid);
            Assert.Equal(string.Empty, modelValidation.GetErrorMessagesAsString());
        }
    }
}
