using Newtonsoft.Json.Linq;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;

namespace WebShopDQ.App.Services
{
    public class EmailService : IEmailService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IGetHTMLBodyRepository _htmlBodyRepository;

        public EmailService(IEmailRepository emailRepository,
            IGetHTMLBodyRepository htmlBodyRepository,
            IAuthenticationRepository authenticationRepository)
        {
            _emailRepository = emailRepository;
            _htmlBodyRepository = htmlBodyRepository;
            _authenticationRepository = authenticationRepository;

        }

        public async Task<bool> SendEmail(EmailMessageModel emailMessageModel)
        {
            return await _emailRepository.SendEmail(emailMessageModel);
        }

        public async Task<bool> SendEmailRegister(string email, string token)
        {
            //var emailModel = await _authenticationRepository.GetConfirmEmail(email);
            var verifLink = "https://localhost:7279" + token;
            var body = await _htmlBodyRepository.GetBody("Verify.html");
            body = body.Replace("[[verilink]]", verifLink);
            var confirmationMail = new EmailMessageModel
            {
                To = email,
                Subject = "Verified your email",
                Body = body
            };
            return await _emailRepository.SendEmail(confirmationMail);
        }
    }
}
