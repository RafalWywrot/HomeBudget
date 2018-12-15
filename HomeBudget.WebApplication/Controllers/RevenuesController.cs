using HomeBudget.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.WebApplication.Controllers
{
    public class RevenuesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Add()
        {
            var model = new FinanceViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(FinanceViewModel model)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }
    }
}