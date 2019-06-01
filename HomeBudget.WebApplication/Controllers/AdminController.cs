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
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin
        public ActionResult Categories()
        {
            return View();
        }
        /// <summary>
        /// When expense is set to true get only expenses categories, otherwise get revenues categories
        /// </summary>
        /// <param name="isExpense">isExpense - costs (minus or plus)</param>
        /// <returns>All categories by expense</returns>
        public ActionResult ShowForCategoryGroup(bool isExpense)
        {
            var categories = _unitOfWork.CategoryRepository.GetOverview(x => x.IsExpense == isExpense).ToList();
            ViewBag.IsExpense = isExpense ? true : false;
            return View(categories);
        }
        /// <summary>
        /// Show form with add category
        /// </summary>
        /// <param name="isExpense">isExpense - costs (minus or plus)</param>
        /// <returns>View with category form</returns>
        public ActionResult AddCategory(bool isExpense)
        {
            ViewBag.CategoryTo = isExpense ? "wydatków" : "przychodów";
            return View(new CategoryViewModel { IsExpense = isExpense });
        }
        /// <summary>
        /// Add category to specific group of categories, when expense is set to true add category for expenses, otherwise add category as revenue
        /// </summary>
        /// <param name="isExpense">isExpense - costs (minus or plus)</param>
        /// <returns>Add category with expense flag</returns>
        [HttpPost]
        public ActionResult AddCategory(CategoryViewModel category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _unitOfWork.CategoryRepository.Add(new Category
            {
                IsExpense = category.IsExpense,
                Name = category.Name
            });
            _unitOfWork.SaveChanges();
            return RedirectToAction("ShowForCategoryGroup", "Admin", new { isExpense = category.IsExpense });
        }

        #region reports
        /// <summary>
        /// Show all types of categories
        /// </summary>
        public ActionResult Reports()
        {
            return View();
        }

        /// <returns>View with general report without specific categories</returns>
        public ActionResult General()
        {
            var model = GetModelForGeneral(DateTime.Now);
            ViewBag.ReportName = "General";
            return View(model);
        }
        [HttpPost]
        public ActionResult General(GeneralReportViewModel model)
        {
            ViewBag.ReportName = "General";
            if (!ModelState.IsValid)
                return View("General", model);

            var newModel = GetModelForGeneral(model.DateReport);
            return View(newModel);
        }
        /// <returns>View with detailed report divided into categories only by one moth</returns>
        public ActionResult Detailed()
        {
            var model = GetModelForDetailed(DateTime.Now);
            ViewBag.ReportName = "Detailed";
            return View(model);
        }
        [HttpPost]
        public ActionResult Detailed(DetailedReportViewModel model)
        {
            ViewBag.ReportName = "Detailed";
            if (!ModelState.IsValid)
            {
                return View("Detailed", model);
            }
            var newmodel = GetModelForDetailed(model.DateReport);
            return View(newmodel);
        }
        /// <returns>View with detailed report divided into categories with specific date range</returns>
        public ActionResult TimeRange()
        {
            ViewBag.ReportName = "TimeRange";
            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var model = GetModelForTimeRange(firstDayOfMonth, lastDayOfMonth);
            return View(model);
        }
        [HttpPost]
        public ActionResult TimeRange(TimeRangeReportViewModel model)
        {
            ViewBag.ReportName = "TimeRange";
            if (!ModelState.IsValid)
            {
                return View("TimeRange", model);
            }
            var newmodel = GetModelForTimeRange(model.DateFrom, model.DateTo);
            return View(newmodel);
        }
        /// <summary>
        /// Get all finances with date range
        /// </summary>
        /// <param name="dateFrom">start from</param>
        /// <param name="dateTo">start to</param>
        private TimeRangeReportViewModel GetModelForTimeRange(DateTime dateFrom, DateTime dateTo)
        {
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= dateFrom && x.TimeEvent <= dateTo).ToList();
            var model = new TimeRangeReportViewModel()
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
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
            ModelState.Remove("DateFrom");
            ModelState.Remove("DateTo");
            return model;
        }
        /// <summary>
        /// Get first day of this month and show all finances divided into categories for this month
        /// </summary>
        /// <param name="dateTime">Get only month for this date</param>
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
        /// <summary>
        /// Get first day of this month and show summary of finances for this month
        /// </summary>
        /// <param name="dateTime">Get only month for this date</param>
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
        #endregion
    }
}