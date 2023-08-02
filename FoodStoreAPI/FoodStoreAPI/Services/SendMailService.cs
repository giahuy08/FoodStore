using FoodStoreAPI.Services.Interfaces;
using FoodStoreAPI.ViewModel.Mail;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FoodStoreAPI.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly EmailSettingViewModel _emailSettings;
        private readonly IHostEnvironment _env;
        public SendMailService(IOptions<EmailSettingViewModel> emailSettings, IHostEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendMailAsync(SendMailViewModel mail)
        {
            string FilePath;
            if (_env.IsDevelopment())
            {
                FilePath = Directory.GetCurrentDirectory() + "\\Settings\\TemplateMail\\Welcome.html";
            }
            else
            {
                FilePath = "/app/Settings/TemplateMail/wellcomePage.html";
            };
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();
            MailText = MailText.Replace("[username]", mail.UserName)
                               .Replace("[email]", mail.ToEmail)
                               .Replace("[confirmLink]", mail.ComfirmEmailLink);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.UserName);
            email.To.Add(MailboxAddress.Parse(mail.ToEmail));
            email.Subject = $"Welcome {mail.UserName}";
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSettings.SmtpServer, _emailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.UserName, _emailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
