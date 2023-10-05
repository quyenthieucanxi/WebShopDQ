using AutoMapper;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;
using WebShopDQ.App.ViewModels.Authentication;

namespace WebShopDQ.App.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterModel, User>().ReverseMap();
            CreateMap<UserInfoViewModel, User>().ReverseMap();
            CreateMap<User, UserInfoViewModel>().ReverseMap();
            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<Post, PostViewModel>()
                .ForMember(destination => destination.CategoryName,
                    options => options.MapFrom(source => source.Category!.CategoryName));
            CreateMap<Category, CategoryViewModel>();
                /*.ForMember(destination => destination.IdCategory,
                    options => options.MapFrom(source => source.Id));*/
        }
    }
}
