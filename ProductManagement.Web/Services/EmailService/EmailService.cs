using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace ProductManagement.Web.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            // Create the email message
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["EmailSettings:SenderEmail"], "XH....AA"),
                Subject = subject,
                Body = body,
                IsBodyHtml = false  // Set to true if sending HTML content
            };

            mailMessage.To.Add(new MailAddress(toEmail));

            using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
            {
                smtpClient.EnableSsl = true;  // Use SSL encryption
                smtpClient.Credentials = new NetworkCredential(
                    _configuration["EmailSettings:SenderEmail"],
                    _configuration["EmailSettings:SenderPassword"]
                );

                smtpClient.Send(mailMessage);
            }
        }
    }
}
