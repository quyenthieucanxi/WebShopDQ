
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit.Text;
using MimeKit;
using Org.BouncyCastle.Asn1.Ocsp;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;

namespace WebShopDQ.App.Services
{
    public class EmailService : IEmailService
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public EmailService(IAuthenticationRepository authenticationRepository,
                        IConfiguration configuration,
                        IWebHostEnvironment webHostEnvironment)
        {
            _authenticationRepository = authenticationRepository;
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> SendEmail(EmailMessageModel emailMessageModel)
        {
            var emailMessgae = new MimeMessage();
            emailMessgae.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUsername").Value));
            emailMessgae.To.Add(MailboxAddress.Parse(emailMessageModel.To));
            emailMessgae.Subject = emailMessageModel.Subject;
            emailMessgae.Body = new TextPart(TextFormat.Html) { Text = emailMessageModel.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUsername").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(emailMessgae);
            smtp.Disconnect(true);
            return await Task.FromResult(true);

        }

        public async Task<bool> SendEmailRegister(string email, string token)
        {

            var verifLink = _configuration["Url"] + token;
            var body = GetBody("Verify.html");
            body = body.Replace("[[verilink]]", verifLink);
            var confirmationMail = new EmailMessageModel
            {
                To = email,
                Subject = "Verified your email",
                Body = body
            };
            return await SendEmail(confirmationMail);
        }

        public async Task<bool> SendEmailForgetPassword(string email, string token)
        {
            var verifLink = _configuration["Url"] + token;
            var body = GetBody("ForgetPassword.html");
            body = body.Replace("[[verilink]]", verifLink);
            var confirmationMail = new EmailMessageModel
            {
                To = email,
                Subject = "Forget password!",
                Body = body
            };
            return await SendEmail(confirmationMail);
        }
        public string GetBody(string type)
        {
            string wwwrootPath = _webHostEnvironment.WebRootPath;
            string bodyContentPath = Path.Combine(wwwrootPath, "Body");
            string fileName = type;
            string filePath = Path.Combine(bodyContentPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                string content = System.IO.File.ReadAllText(filePath);
                return content;
            }
            return string.Empty;
        }
    }
}
