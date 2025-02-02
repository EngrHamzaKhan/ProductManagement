namespace ProductManagement.Web.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(string toEmail, string subject, string body);
    }
}
