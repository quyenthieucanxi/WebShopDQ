﻿using Microsoft.Extensions.DependencyInjection;
using WebShopDQ.App.Services;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Dependency
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddServices(this IServiceCollection service)
        {
            service.AddTransient<IAuthenticationService, AuthenticationService>();
            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<ITokenInfoService, TokenInfoService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IPostService, PostService>();
            service.AddTransient<ICategoryService, CategoryService>();
            service.AddTransient<IFriendshipService, FriendshipService>();
            service.AddTransient<IOrderService, OrderService>();
            service.AddTransient<IFileService, FileService>();
            service.AddTransient<IAddressShippingService, AddressShippingService>();
            return service;
        }
    }
}
