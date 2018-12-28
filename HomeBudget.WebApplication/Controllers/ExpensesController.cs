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
    public class ExpensesController : BaseController
    {
        public ActionResult Index()
        {
            var categories = _unitOfWork.CategoryRepository.GetOverview(x => x.IsExpense).ToList();
            return View(categories);
        }
        public ActionResult ShowForCategory(int categoryId)
        {
            var reveneus = _unitOfWork.FinanceRepository.GetOverview(x => x.CategoryId == categoryId).ToList();
            var model = Mapper.Map<List<RevenueViewModel>>(reveneus);
            ViewBag.CategoryId = categoryId;
            return View(model);
        }
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
        [HttpPost]
        public ActionResult Add(RevenueViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDAO = Mapper.Map<Finance>(model);
                _unitOfWork.FinanceRepository.Add(modelDAO);
                _unitOfWork.SaveChanges();
                return RedirectToAction("ShowForCategory", new { categoryId = model.CategoryId });
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == id);
            var model = Mapper.Map<RevenueViewModel>(finanse);
            return View(model);
        }
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
            return View(model);
        }
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