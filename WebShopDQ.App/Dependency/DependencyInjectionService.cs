using Microsoft.Extensions.DependencyInjection;
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
            return service;
        }
    }
}
