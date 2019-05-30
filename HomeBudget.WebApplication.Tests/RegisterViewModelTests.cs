using HomeBudget.WebApplication.Models;
using Xunit;

namespace HomeBudget.WebApplication.Tests
{
    public class RegisterViewModelTests
    {
        [Fact]
        public void RegisterViewModel_validation_should_return_invalid_when_all_properties_are_empty()
        {
            var model = new RegisterViewModel();
            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
        }
        [Fact]
        public void RegisterViewModel_validation_should_return_invalid_when_name_empty()
        {
            var model = new RegisterViewModel()
            {
                Name = string.Empty
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Imię jest wymagane", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void RegisterViewModel_validation_should_return_invalid_when_last_name_empty()
        {
            var model = new RegisterViewModel()
            {
                LastName = string.Empty
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Nazwisko jest wymagane", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void RegisterViewModel_validation_should_return_invalid_when_email_empty()
        {
            var model = new RegisterViewModel()
            {
                Email = string.Empty
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Email jest wymagane", modelValidation.GetErrorMessagesAsString());
        }
        [Theory]
        [InlineData("plainaddress")]
        [InlineData("#@%^%#$@#$@#.com")]
        [InlineData("@domain.com")]
        [InlineData("Joe Smith <email@domain.com>")]
        [InlineData("email.domain.com")]
        [InlineData("email@domain@domain.com")]
        [InlineData(".email@domain.com")]
        [InlineData("email.@domain.com")]
        [InlineData("email..email@domain.com")]
        [InlineData("email@domain.com (Joe Smith)")]
        [InlineData("email@domain")]
        [InlineData("email@-domain.com")]
        [InlineData("email@111.222.333.44444")]
        [InlineData("email@domain..com")]
        public void RegisterViewModel_validation_should_return_invalid_when_email_invalid_format(string email)
        {
            var model = new RegisterViewModel()
            {
                Email = email
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Wartość w polu Email nie jest prawidłowym adresem e-mail", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void RegisterViewModel_validation_should_return_invalid_when_password_empty()
        {
            var model = new RegisterViewModel()
            {
                Password = string.Empty
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Hasło jest wymagane", modelValidation.GetErrorMessagesAsString());
        }
        [Theory]
        [InlineData("1")]
        [InlineData("ffff5")]
        [InlineData("ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5ffff5fff101")]
        public void RegisterViewModel_validation_should_return_invalid_when_password_invalid_length(string password)
        {
            var model = new RegisterViewModel()
            {
                Password = password
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Hasło musi mieć przynajmniej 6 znaków", modelValidation.GetErrorMessagesAsString());
        }
        
        [Theory]
        [InlineData("password", "password2")]
        [InlineData("password2", "password")]
        public void RegisterViewModel_validation_should_return_invalid_when_password_is_different_than_confirm_password(string password, string confirmPassword)
        {
            var model = new RegisterViewModel()
            {
                Password = password,
                ConfirmPassword = confirmPassword
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.False(valid);
            Assert.Contains("Wpisane hasła nie są takie same", modelValidation.GetErrorMessagesAsString());
        }
        [Fact]
        public void RegisterViewModel_validation_should_return_valid_when_all_properties_valid_format()
        {
            var model = new RegisterViewModel()
            {
                Name = "a",
                LastName = "a",
                Email = "abra@o2.pl",
                Password = "password",
                ConfirmPassword = "password"
            };

            var modelValidation = new ValidationModel(model);
            var valid = modelValidation.IsValid();
            Assert.True(valid);
            Assert.Equal(string.Empty, modelValidation.GetErrorMessagesAsString());
        }
    }
}
