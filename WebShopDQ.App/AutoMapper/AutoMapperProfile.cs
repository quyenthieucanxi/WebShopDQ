﻿using AutoMapper;
using WebShopDQ.App.Models;
using WebShopDQ.App.Models.Authentication;

namespace WebShopDQ.App.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>().ReverseMap();
        }
    }
}