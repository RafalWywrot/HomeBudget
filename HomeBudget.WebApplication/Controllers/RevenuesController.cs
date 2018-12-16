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
    public class RevenuesController : BaseController
    {
        public ActionResult Index()
        {
            var categories = _unitOfWork.CategoryRepository.GetOverview(x => !x.IsExpense).ToList();
            return View(categories);
        }
        public ActionResult ShowForCategory(int categoryId)
        {
            var reveneus = _unitOfWork.FinanceRepository.GetOverview(x => x.CategoryId == categoryId).ToList();
            var model = new List<FinanceViewModel>();
            foreach (var revenue in reveneus)
            {
                model.Add(new FinanceViewModel()
                    {
                        Id = revenue.Id,
                        CategoryId = categoryId,
                        CategoryName = revenue.Category.Name,
                        Name = revenue.Name,
                        Price = revenue.Value
                    });
            }
            ViewBag.CategoryId = categoryId;
            return View(model);
        }
        [HttpGet]
        public ActionResult Add(int categoryId)
        {
            var category = _unitOfWork.CategoryRepository.GetDetail(x => x.Id == categoryId);
            var model = new FinanceViewModel()
            {
                CategoryId = category.Id,
                CategoryName = category.Name
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(FinanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var modelDao = new Finance()
                {
                    Value = model.Price,
                    CategoryId = model.CategoryId,
                    Name = model.Name
                };
                _unitOfWork.FinanceRepository.Add(modelDao);
                _unitOfWork.SaveChanges();
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == id);
            var model = new FinanceViewModel()
            {
                Id = finanse.Id,
                CategoryId = finanse.Category.Id,
                CategoryName = finanse.Category.Name,
                Name = finanse.Name,
                Price = finanse.Value
            };
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(FinanceViewModel model)
        {
            if (ModelState.IsValid)
            {
                var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == model.Id);
                finanse.Name = model.Name;
                finanse.Value = model.Price;
                finanse.CreateDateTime = DateTime.Now;
                _unitOfWork.SaveChanges();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var finanse = _unitOfWork.FinanceRepository.GetDetail(x => x.Id == id);
            _unitOfWork.FinanceRepository.Delete(finanse);
            _unitOfWork.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}