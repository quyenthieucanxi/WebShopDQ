
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;


namespace WebShopDQ.App.Services
{
    public class EmailService : IEmailService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IEmailRepository _emailRepository;
        private readonly IGetHTMLBodyRepository _htmlBodyRepository;
        private readonly IConfiguration _configuration;

        public EmailService(IEmailRepository emailRepository,
            IGetHTMLBodyRepository htmlBodyRepository,
            IAuthenticationRepository authenticationRepository,
            IConfiguration configuration)
        {
            _emailRepository = emailRepository;
            _htmlBodyRepository = htmlBodyRepository;
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
        }

        public async Task<bool> SendEmail(EmailMessageModel emailMessageModel)
        {
            return await _emailRepository.SendEmail(emailMessageModel);
        }

        public async Task<bool> SendEmailRegister(string email, string token)
        {
            //var emailModel = await _authenticationRepository.GetConfirmEmail(email);
           
            var verifLink = _configuration["Url"] + token;
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

        public async Task<bool> SendEmailForgetPassword(string email, string token)
        {
            //var emailModel = await _authenticationRepository.GetConfirmEmail(email);

            var verifLink = _configuration["Url"] + token;
            var body = await _htmlBodyRepository.GetBody("ForgetPassword.html");
            body = body.Replace("[[verilink]]", verifLink);
            var confirmationMail = new EmailMessageModel
            {
                To = email,
                Subject = "Forget password!",
                Body = body
            };
            return await _emailRepository.SendEmail(confirmationMail);
        }
    }
}
