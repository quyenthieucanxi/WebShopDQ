using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        private readonly IConfiguration _configuration;

        public EmailRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }
}
