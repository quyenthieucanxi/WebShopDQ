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
                    options => options.MapFrom(source => source.Category!.CategoryName))
                .ForMember(destination => destination.UserId,
                    options => options.MapFrom(source => source.User!.Id))
                .ForMember(destination => destination.AvatarUrl,
                    options => options.MapFrom(source => source.User!.AvatarUrl));
            CreateMap<Category, CategoryViewModel>();
            /*.ForMember(destination => destination.IdCategory,
                options => options.MapFrom(source => source.Id));*/
            CreateMap<Order, OrderViewModel>();
            CreateMap<UserInfoDTO, UserDTO>().ReverseMap();
        }
    }
}
