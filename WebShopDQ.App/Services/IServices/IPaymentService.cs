using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IPaymentService
    {
       public Task<string> CreateUrlPayment(OrderDTO order,Guid userId);
       public Task<PaymentViewModel> CallbackPayment(VNPayDTO vNPayDTO);
    }
}
