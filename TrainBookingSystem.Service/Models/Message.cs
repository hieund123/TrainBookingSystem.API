using MimeKit;

namespace TrainBookingSystem.Service.Models
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string content)
        {
            To = to.Select(email => new MailboxAddress(name: "", address: email)).ToList();
            Subject = subject;
            Content = content;
        }
    }
}
