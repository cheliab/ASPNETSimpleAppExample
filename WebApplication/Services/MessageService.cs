namespace WebApplication.Services
{
    public interface IMessageSender
    {
        public string Send();
    }

    public class EmailSender : IMessageSender
    {
        public string Send()
        {
            return "Email message";
        }
    }
    public class MessageService
    {
        private IMessageSender _messageSender;

        public MessageService(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        public string GetMessage()
        {
            return _messageSender.Send();
        }
    }
}