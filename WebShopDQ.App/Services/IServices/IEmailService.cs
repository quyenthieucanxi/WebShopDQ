using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailMessageModel emailMessageModel);
        Task<bool> SendEmailRegister(string email, string token);
        Task<bool> SendEmailForgetPassword(string email, string token);
    }
}
