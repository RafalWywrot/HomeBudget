using HomeBudget.Domain;
using HomeBudget.Domain.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        protected int GetUserInfoId()
        {
            var userId = User.Identity.GetUserId();
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(userId).UserInfo;
            return user.Id;
        }
    }
}