using AutoMapper;
using HomeBudget.Database;
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
            CreateMap<DAO.Finance, RevenueViewModel>()
                .ForMember(
                    dest => dest.Price,
                    opt => opt.MapFrom(src => src.Value)
                )
                .ForMember(
                    dest => dest.CategoryId,
                    opt => opt.MapFrom(src => src.Category.Id)
                )
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.Category.Name)
                )
                .ForMember(
                    dest => dest.CreateDateTimeDatabase,
                    opt => opt.MapFrom(src => src.CreateDateTime)
                )
                .AfterMap((src, dest) => dest.Price = Math.Round(dest.Price, 2));
            CreateMap<RevenueViewModel, DAO.Finance>()
              .ForMember(
                  dest => dest.Value,
                  opt => opt.MapFrom(src => src.Price)
              );
            CreateMap<RevenueViewModel, DAO.Category>()
              .ForMember(
                  dest => dest.Id,
                  opt => opt.MapFrom(src => src.CategoryId)
              );
        }
    }
}