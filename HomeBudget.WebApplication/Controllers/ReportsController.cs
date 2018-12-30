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
            var model = GetModelForGeneral(DateTime.Now);
            return View(model);
        }
        [HttpPost]
        public ActionResult General(GeneralReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newModel = GetModelForGeneral(model.DateReport);
            //DateTime date = (DateTime)model.DateReport;
            //var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            //var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            //var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= firstDayOfMonth && x.TimeEvent <= lastDayOfMonth).ToList();
            //model.AnyCashOperation = false;
            //if (finanses.Any())
            //{
            //    model.Revenues = finanses.Where(x => !x.Category.IsExpense).Select(x => x.Value).Sum();
            //    model.Expenses = finanses.Where(x => x.Category.IsExpense).Select(x => x.Value).Sum();
            //    model.AnyCashOperation = true;
            //}
            
            
            return View(newModel);
        }
        private GeneralReportViewModel GetModelForGeneral(DateTime dateTime)
        {
            DateTime date = (DateTime)dateTime;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= firstDayOfMonth && x.TimeEvent <= lastDayOfMonth).ToList();
            var model = new GeneralReportViewModel()
            {
                DateReport = dateTime,
                AnyCashOperation = false
            };
            if (finanses.Any())
            {
                model.Revenues = finanses.Where(x => !x.Category.IsExpense).Select(x => x.Value).Sum();
                model.Expenses = finanses.Where(x => x.Category.IsExpense).Select(x => x.Value).Sum();
                model.AnyCashOperation = true;
            }
            ModelState.Clear();
            ModelState.Remove("DateReport");
            return model;
        }
        public ActionResult Detailed()
        {
            var model = GetModelForDetailed(DateTime.Now);
            return View(model);
        }
        [HttpPost]
        public ActionResult Detailed(DetailedReportViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var newmodel = GetModelForDetailed(model.DateReport);
            return View(newmodel);
        }
        private DetailedReportViewModel GetModelForDetailed(DateTime date)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= firstDayOfMonth && x.TimeEvent <= lastDayOfMonth).ToList();
            var model = new DetailedReportViewModel()
            {
                DateReport = date,
                AnyCashOperation = false,
                Categories = _unitOfWork.CategoryRepository.GetOverview().ToList()
            };
            if (finanses.Any())
            {
                model.Revenues = Mapper.Map<List<RevenueViewModel>>(finanses.Where(x => !x.Category.IsExpense).ToList());
                model.Expenses = Mapper.Map<List<RevenueViewModel>>(finanses.Where(x => x.Category.IsExpense).ToList());
                model.AnyCashOperation = true;
            }
            ModelState.Clear();
            ModelState.Remove("DateReport");
            return model;
        }
    }
}