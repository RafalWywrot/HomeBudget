using HomeBudget.WebApplication.Controllers;
using HomeBudget.WebApplication.Models;
using System.Web.Mvc;
using Xunit;
namespace HomeBudget.WebApplication.Tests
{
    public class ReportsControllerTests
    {
        [Fact]
        public void General_should_return_general_view_when_model_state_invalid()
        {
            var controller = new ReportsController(new FakeUnitOfWork());
            controller.ModelState.AddModelError("", "");
            ViewResult result = (ViewResult)controller.General(new GeneralReportViewModel());
            Assert.Equal("General", result.ViewName);
        }
        [Fact]
        public void Detailed_should_return_detailed_view_when_model_state_invalid()
        {
            var controller = new ReportsController(new FakeUnitOfWork());
            controller.ModelState.AddModelError("", "");
            ViewResult result = (ViewResult)controller.Detailed(new DetailedReportViewModel());
            Assert.Equal("Detailed", result.ViewName);
        }
        [Fact]
        public void Timerange_should_return_timerange_view_when_model_state_invalid()
        {
            var controller = new ReportsController(new FakeUnitOfWork());
            controller.ModelState.AddModelError("", "");
            ViewResult result = (ViewResult)controller.TimeRange(new TimeRangeReportViewModel());
            Assert.Equal("TimeRange", result.ViewName);
        }
    }
}
