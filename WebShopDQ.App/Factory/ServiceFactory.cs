using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;

namespace WebShopDQ.App.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        //public IEmailService GetServiceEmail(TypeSendMail type)
        //{
        //    return type switch
        //    {
        //        TypeSendMail.Register => _serviceProvider.GetService<ServiceA>(),
        //        TypeSendMail.ForgetPassword => _serviceProvider.GetService<ServiceB>(),
        //        _ => throw new NotSupportedException($"Service type '{type}' is not supported")
        //    };
        //}
    }
}
