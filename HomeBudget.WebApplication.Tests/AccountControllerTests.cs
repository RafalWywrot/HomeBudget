using HomeBudget.WebApplication.Controllers;
using HomeBudget.WebApplication.Models;
using System.Web.Mvc;
using Xunit;

namespace HomeBudget.WebApplication.Tests
{
    public class AccountControllerTests
    {
        [Fact]
        public void Login_should_return_login_view()
        {
            var controller = new AccountController();
            ViewResult result = (ViewResult)controller.Login("");
            Assert.Equal("Login", result.ViewName);
        }
        [Fact]
        public void Login_should_return_login_view_when_model_state_invalid()
        {
            var controller = new AccountController();
            controller.ModelState.AddModelError("login", "invalid email");
            ViewResult result =  (ViewResult)controller.Login(new LoginViewModel(), "").Result;
            Assert.Equal("Login", result.ViewName);
        }
        [Fact]
        public void Register_should_return_register_view()
        {
            var controller = new AccountController();
            ViewResult result = (ViewResult)controller.Register();
            Assert.Equal("Register", result.ViewName);
        }
        [Fact]
        public void Register_should_return_register_view_when_model_state_invalid()
        {
            var controller = new AccountController();
            controller.ModelState.AddModelError("Register", "name required");
            ViewResult result = (ViewResult)controller.Register(new RegisterViewModel()).Result;
            Assert.Equal("Register", result.ViewName);
        }
    }
}
