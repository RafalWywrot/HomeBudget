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
    }
}