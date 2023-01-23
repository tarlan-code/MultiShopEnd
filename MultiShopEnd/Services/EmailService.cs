using MultiShopEnd.Abstractions.Email;
using System.Net;
using System.Net.Mail;

namespace MultiShopEnd.Services
{
    public class EmailService : IEmailService
    {

        readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string email, string subject, string body, bool IsHtml = false)
        {
           SmtpClient smtp = new SmtpClient(_configuration["Email:Server"], Convert.ToInt32(_configuration["Email:Port"]));
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration["Email:Mail"], _configuration["Email:Password"]);

            MailAddress from = new MailAddress(_configuration["Email:Mail"],"MultiShop");
            MailAddress to = new MailAddress(email);
            MailMessage message = new MailMessage(from,to);
            message.Subject = subject;
            message.Body = body;

            smtp.Send(message);

        }
    }
}
