using AutoMapper;
using HomeBudget.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAO = HomeBudget.Database;

namespace HomeBudget.WebApplication.Helpers
{

    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DAO.Category, FinanceViewModel>();
        }
    }
}