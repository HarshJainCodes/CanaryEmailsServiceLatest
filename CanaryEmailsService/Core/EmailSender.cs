using System.Net.Mail;
using System.Net;

namespace CanaryEmailsService.Core
{
    public interface IEmailSender
    {
        Task SendEmail(string to, string subject, string body);
    }

    public class EmailSender: IEmailSender
    {
        private ILogger<EmailSender> _logger;
        private IConfiguration _configuration;

        public EmailSender(ILogger<EmailSender> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            string fromEmail = _configuration.GetSection("FromEmail").Value;
            string password = _configuration.GetSection("EmailPass").Value;
            string smtpServer = "smtp.gmail.com";
            int smtpPort = 587;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(fromEmail);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            var smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                Credentials = new NetworkCredential(fromEmail, password),
                EnableSsl = true
            };

            try
            {
                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Exception Occured {ex.ToString()}");
            }
            _logger.LogInformation($"Sending email to {to} with subject: {subject} and body: {body}");
        }
    }
}
