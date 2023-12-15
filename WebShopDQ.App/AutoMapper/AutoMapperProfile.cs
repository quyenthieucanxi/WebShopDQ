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
            CreateMap<User, UserInfoViewModel>().ReverseMap();
                //.ForMember(destination => destination.Role,
                //options => options.MapFrom(source => source))
                
            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<Post, PostViewModel>()
                .ForMember(destination => destination.CategoryName,
                    options => options.MapFrom(source => source.Category!.CategoryName))
                .ForMember(destination => destination.UserId,
                    options => options.MapFrom(source => source.User!.Id))
                .ForMember(destination => destination.AvatarUrl,
                    options => options.MapFrom(source => source.User!.AvatarUrl))
                .ForMember(destination => destination.User,
                    options => options.MapFrom(source => source.User));
                
            CreateMap<Category, CategoryViewModel>();
            /*.ForMember(destination => destination.IdCategory,
                options => options.MapFrom(source => source.Id));*/
            CreateMap<Order, OrderViewModel>();
            CreateMap<UserInfoDTO, UserDTO>().ReverseMap();
            CreateMap<AddressShipping, AddressShippingDTO>().ReverseMap();
            CreateMap<AddressShipping, AddressShippngViewModel>().ReverseMap();
            CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<Order, OrderViewModel>()
                .ForMember(destination => destination.User,
                    options => options.MapFrom(source => source.UserOrder))
                .ForMember(destination => destination.Product,
                    options => options.MapFrom(source => source.Product))
                .ForMember(destination => destination.RecipientName,
                    options => options.MapFrom(source => source.AddressShipping.RecipientName))
                .ForMember(destination => destination.Phone,
                    options => options.MapFrom(source => source.AddressShipping.Phone))
                .ForMember(destination => destination.AddressShipping,
                    options => options.MapFrom(source =>
                       $"{source.AddressShipping.AddressDetail}, {source.AddressShipping.Distrist}, {source.AddressShipping.Province}"));

        }
    }
}
