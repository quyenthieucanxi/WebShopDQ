using WebShopDQ.App.Models;

namespace WebShopDQ.App.Services.IServices
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailMessageModel emailMessageModel);
        Task<bool> SendEmailRegister(string email, string token);
    }
}
