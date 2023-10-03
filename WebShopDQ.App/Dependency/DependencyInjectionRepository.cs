using InsternShip.Data;
using Microsoft.Extensions.DependencyInjection;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Dependency
{
    public static class DependencyInjectionRepository
    {
        public static IServiceCollection AddRepository(this IServiceCollection service) 
        {
            service.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork));
            service.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            service.AddTransient<IAuthenticationRepository, AuthenticationRepository>();
            service.AddTransient<IGetHTMLBodyRepository, GetHTMLBodyRepository>();
            service.AddTransient<IEmailRepository, EmailRepository>();
            service.AddTransient<IDecodeRepository, DecodeRepository>();
            service.AddTransient<IUserRepository, UserRepository>();
            return service;
        }
    }
}
