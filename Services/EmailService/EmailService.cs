
using System;
using System.Threading.Tasks;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;
using OnBusyness.Services.EmailService.Models;

namespace OnBusyness.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private Guid guid { get; set; } = Guid.NewGuid();

        public EmailService(IConfiguration config, ILogger<EmailService> logger)
        {
            _config = config;
        }

        public async Task<bool> SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            // ! SHOULD CHECK WHEN IT IS DISPOSING
            string response = "";
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                response = await smtp.SendAsync(email);
                smtp.Disconnect(true);
            };
            return response.Contains("OK");
        }
    }
}