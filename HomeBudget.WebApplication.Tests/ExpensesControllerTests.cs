using HomeBudget.WebApplication.Controllers;
using HomeBudget.WebApplication.Models;
using System.Web.Mvc;
using Xunit;
namespace HomeBudget.WebApplication.Tests
{
    public class ExpensesControllerTests
    {
        [Fact]
        public void Add_should_return_add_view_when_model_state_invalid()
        {
            var controller = new ExpensesController(new FakeUnitOfWork());
            controller.ModelState.AddModelError("", "");
            ViewResult result = (ViewResult)controller.Add(new RevenueViewModel());
            Assert.Equal("Add", result.ViewName);
        }
        [Fact]
        public void Edit_should_return_Edit_view_when_model_state_invalid()
        {
            var controller = new ExpensesController(new FakeUnitOfWork());
            controller.ModelState.AddModelError("", "");
            ViewResult result = (ViewResult)controller.Edit(new RevenueViewModel());
            Assert.Equal("Edit", result.ViewName);
        }
    }
}
