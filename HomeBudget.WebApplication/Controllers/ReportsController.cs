﻿using AutoMapper;
using HomeBudget.Database;
using HomeBudget.Domain.Repositories;
using HomeBudget.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace HomeBudget.WebApplication.Controllers
{
    [Authorize]
    public class ReportsController : BaseController
    {
        public ReportsController() { }
        public ReportsController(IUnitOfWork unitOfWork) : base(unitOfWork) { }
        /// <summary>
        /// Get all report options
        /// </summary>
        public ActionResult Index()
        {
            return View();
        }
        /// <returns>View with general report without specific categories restrict by user id</returns>
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
            {
                return View("General", model);
            }
            var newModel = GetModelForGeneral(model.DateReport);
            return View(newModel);
        }
        /// <summary>
        /// Get first day of this month and show all finances divided into categories for this month restrict by user id
        /// </summary>
        /// <param name="dateTime">Get only month for this date</param>
        private GeneralReportViewModel GetModelForGeneral(DateTime dateTime)
        {
            DateTime date = (DateTime)dateTime;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var userId = GetUserInfoId();
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= firstDayOfMonth && x.TimeEvent <= lastDayOfMonth && x.UserInfoId == userId).ToList();
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
        /// <returns>View with detailed report divided into categories only by one moth restrict by user id</returns>
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
        private DetailedReportViewModel GetModelForDetailed(DateTime date)
        {
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var userId = GetUserInfoId();
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= firstDayOfMonth && x.TimeEvent <= lastDayOfMonth && x.UserInfoId == userId).ToList();
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
        /// <returns>View with detailed report divided into categories with specific date range restrict by user id</returns>
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
        /// Get all finances with date range restrict by user id
        /// </summary>
        /// <param name="dateFrom">start from</param>
        /// <param name="dateTo">start to</param>
        private TimeRangeReportViewModel GetModelForTimeRange(DateTime dateFrom, DateTime dateTo)
        {
            var userId = GetUserInfoId();
            var finanses = _unitOfWork.FinanceRepository.GetOverview(x => x.TimeEvent >= dateFrom && x.TimeEvent <= dateTo && x.UserInfoId == userId).ToList();
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
    }
}