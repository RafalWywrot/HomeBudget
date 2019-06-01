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
        // GET: Admin
        public ActionResult ShowForCategoryGroup(bool isExpense)
        {
            var categories = _unitOfWork.CategoryRepository.GetOverview(x => x.IsExpense == isExpense).ToList();
            ViewBag.IsExpense = isExpense ? true : false;
            return View(categories);
        }
        public ActionResult AddCategory(bool isExpense)
        {
            ViewBag.CategoryTo = isExpense ? "wydatków" : "przychodów";
            return View(new CategoryViewModel { IsExpense = isExpense });
        }
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
        public ActionResult Reports()
        {
            return View();
        }
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