namespace DocuSignLibrary.Models
{
    public class EmailTemplate
    {
        public EmailTemplate(string subject, string messageBody)
        {
            this.Subject = subject;
            this.MessageBody = messageBody;
        }

        public string Subject { get; set; }

        public string MessageBody { get; set; }
    }
}
