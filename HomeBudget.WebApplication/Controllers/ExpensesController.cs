using AutoMapper;
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
    public class ExpensesController : BaseController
    {
        public ExpensesController()
        {

        }
        public ExpensesController(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        /// <summary>
        /// Get all categories with expense flag set to true
        /// </summary>
        public ActionResult Index()
        {
            var categories = _unitOfWork.CategoryRepository.GetOverview(x => x.IsExpense).ToList();
            return View(categories);
        }
        /// <summary>
        /// Get all finances for category limited by user id
        /// </summary>
        /// <param name="categoryId">Category id</param>
        /// <returns>Finances for category and all categories options</returns>
        public ActionResult ShowForCategory(int categoryId)
        {
            var userId = GetUserInfoId();
            var expenses = _unitOfWork.FinanceRepository.GetOverview(x => x.CategoryId == categoryId && x.UserInfoId == userId).ToList();
            var model = new RevenueWithCategoryViewModel
            {
                Revenues = Mapper.Map<List<RevenueViewModel>>(expenses),
                Categories = _unitOfWork.CategoryRepository.GetOverview(x => x.IsExpense).ToList()
            };
            ViewBag.CategoryId = categoryId;
            return View(model);
        }
        /// <summary>
        /// Show form with add expense - name, price and datetime of expense
        /// <param name="categoryId">Category id</param>
        /// <returns>Form for add new expense</returns>
        [HttpGet]
        public ActionResult Add(int categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetDetail(x => x.Id == categoryId);
            var model = new RevenueViewModel()
            {
                CategoryId = category.Id,
                CategoryName = category.Name
            };
            return View(model);
        }
        /// <summary>
        /// Return view when model is invalid
        /// Add new expense when model is valid
        /// </summary>
        /// <param name="model">Model with name and price</param>
        [HttpPost]
        public ActionResult Add(RevenueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDAO = Mapper.Map<Finance>(model);
                modelDAO.UserInfoId = GetUserInfoId();
                _unitOfWork.FinanceRepository.Add(modelDAO);
                _unitOfWork.SaveChanges();
                return RedirectToAction("ShowForCategory", new { categoryId = model.CategoryId });
            }
            return View("Add", model);
        }
        /// <summary>
        /// Show form with edit expense - name, price and datetime of expense
        /// </summary>
        /// <param name="id">Finance id</param>
        /// <returns>Form for edit expense</returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == id);
            var model = Mapper.Map<RevenueViewModel>(finanse);
            return View(model);
        }
        /// <summary>
        /// Return view when model is invalid
        /// Edit expense when model is valid
        /// </summary>
        /// <param name="model">Model with name and price</param>
        [HttpPost]
        public ActionResult Edit(RevenueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == model.Id);
                finanse.Name = model.Name;
                finanse.Value = model.Price;
                finanse.CreateDateTime = DateTime.Now;
                finanse.TimeEvent = model.TimeEvent;
                _unitOfWork.SaveChanges();
                return RedirectToAction("ShowForCategory", new { categoryId = model.CategoryId });
            }
            return View("Edit", model);
        }
        /// <summary>
        /// Delete finance with expense category
        /// </summary>
        /// <param name="id">Finance id</param>
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == id);
            _unitOfWork.FinanceRepository.Delete(finanse);
            _unitOfWork.SaveChanges();
            return RedirectToAction("ShowForCategory", new { categoryId = finanse.CategoryId });
        }
    }
}