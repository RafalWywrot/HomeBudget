using HomeBudget.WebApplication.Controllers;
using HomeBudget.WebApplication.Models;
using System.Web.Mvc;
using Xunit;


namespace HomeBudget.WebApplication.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_should_return_index_view()
        {
            var controller = new HomeController();
            ViewResult result = (ViewResult)controller.Index();
            Assert.Equal("Index", result.ViewName);
        }
        [Fact]
        public void Unathorized_should_return_before_login_view()
        {
            var controller = new HomeController();
            controller.ModelState.AddModelError("Register", "name required");
            ViewResult result = (ViewResult)controller.Unathorized();
            Assert.Equal("BeforeLogin", result.ViewName);
        }
    }
}
