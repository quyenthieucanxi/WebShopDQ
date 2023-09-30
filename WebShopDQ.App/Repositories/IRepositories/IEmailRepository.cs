using WebShopDQ.App.Models;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IEmailRepository
    {
        Task<bool> SendEmail(EmailMessageModel emailMessageModel);
    }
}
