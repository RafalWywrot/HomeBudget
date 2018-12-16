using HomeBudget.Domain.Interfaces;
using HomeBudget.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeBudget.WebApplication.Controllers
{
    public class BaseController : Controller
    {
        protected UnitOfWork _unitOfWork = null;
        public BaseController()
        {
            _unitOfWork = new UnitOfWork();
        }
    }
}