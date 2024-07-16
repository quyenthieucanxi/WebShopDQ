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
            CreateMap<Shop, ShopDTO>().ReverseMap();   
            CreateMap<User, UserInfoDTO>().ReverseMap();
            CreateMap<Files, FilesViewModel>();
            CreateMap<Post, PostViewModel>()
                .ForMember(destination => destination.CategoryName,
                    options => options.MapFrom(source => source.Category!.CategoryName))
                .ForMember(destination => destination.CategoryPath,
                    options => options.MapFrom(source => source.Category!.CategoryPath))
                .ForMember(destination => destination.UserId,
                    options => options.MapFrom(source => source.User!.Id))
                .ForMember(destination => destination.AvatarUrl,
                    options => options.MapFrom(source => source.User!.AvatarUrl))
                .ForMember(destination => destination.User,
                    options => options.MapFrom(source => source.User))
                .ForMember(destination => destination.Files,
                    options => options.MapFrom(source => source.Files)); ;
                
            CreateMap<Shop,ShopViewModel>()
                .ForMember(destination => destination.User,
                    options => options.MapFrom(source => source.User));
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryViewModel>();
            /*.ForMember(destination => destination.IdCategory,
                options => options.MapFrom(source => source.Id));*/
            CreateMap<OrderReviews,OrderReviewDTO>().ReverseMap();
            CreateMap<UserInfoDTO, UserDTO>().ReverseMap();
            CreateMap<AddressShipping, AddressShippingDTO>().ReverseMap();
            CreateMap<AddressShipping, AddressShippngViewModel>();
            CreateMap<Notify, NotifyDTO>().ReverseMap();
            CreateMap<Notify, NotifyViewModel>()
                .ForMember(destination => destination.UserSender,
                    options => options.MapFrom(source => source.UserSender));
            CreateMap<Order,OrderDTO>().ReverseMap();
            CreateMap<Order, OrderViewModel>()
                .ForMember(destination => destination.User,
                    options => options.MapFrom(source => source.UserOrder))
                .ForMember(destination => destination.Products,
                    options => options.MapFrom(source => source.Products))
                .ForMember(destination => destination.RecipientName,
                    options => options.MapFrom(source => source.AddressShipping.RecipientName))
                .ForMember(destination => destination.Phone,
                    options => options.MapFrom(source => source.AddressShipping.Phone))
                .ForMember(destination => destination.AddressShipping,
                    options => options.MapFrom(source =>
                       $"{source.AddressShipping.AddressDetail}, {source.AddressShipping.Distrist}, {source.AddressShipping.Province}"));
            CreateMap<OrderReviews, OrderReviewsViewModel>()
                .ForMember(destination => destination.User,
                    options => options.MapFrom(source => source.Order.UserOrder))
                .ForMember(destiantion => destiantion.Products,
                    options => options.MapFrom(source => source.Order.Products));
            CreateMap<Chats, ChatViewModel>()
               .ForMember(destination => destination.Receiver,
                   options => options.MapFrom(source => source.Receiver))
               .ForMember(destination => destination.Sender,
                   options => options.MapFrom(source => source.Sender))
               .ForMember(destination => destination.Messages,
                   options => options.MapFrom(source => source.Messages));
        }
    }
}
