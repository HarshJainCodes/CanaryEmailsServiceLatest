namespace CanaryEmailsService.Contracts
{
    public class SendEmailMessage : ISendEmailMessage
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public interface ISendEmailMessage
    {
        string ToEmail { get; set; }

        string Subject { get; set; }

        string Body { get; set; }
    }
}
