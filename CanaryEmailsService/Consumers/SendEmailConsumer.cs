using CanaryEmailsService.Contracts;
using CanaryEmailsService.Core;
using MassTransit;

namespace CanaryEmailsService.Consumers
{
    public class SendEmailConsumer: IConsumer<ISendEmailMessage>
    {
        private IEmailSender _emailSender;

        public SendEmailConsumer(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public async Task Consume(ConsumeContext<ISendEmailMessage> context)
        {
            string toEmail = context.Message.ToEmail;
            string subject = context.Message.Subject;
            string body = context.Message.Body;

            await _emailSender.SendEmail(toEmail, subject, body);
        }
    }
}
