using AutoMapper;
using HomeBudget.Database;
using HomeBudget.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HomeBudget.WebApplication.Controllers
{
    public class ReportsController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult General()
        {
            var model = new GeneralReportViewModel()
            {
                DateReport = DateTime.Today
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult General(GeneralReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            DateTime date = (DateTime)model.DateReport;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent > firstDayOfMonth && x.TimeEvent < lastDayOfMonth).ToList();
            model.AnyCashOperation = false;
            if (finanses.Any())
            {
                model.Revenues = finanses.Where(x => !x.Category.IsExpense).Select(x => x.Value).Sum();
                model.Expenses = finanses.Where(x => x.Category.IsExpense).Select(x => x.Value).Sum();
                model.AnyCashOperation = true;
            }
            return View(model);
        }
    }
}