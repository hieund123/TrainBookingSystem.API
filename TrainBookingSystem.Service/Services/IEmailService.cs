using TrainBookingSystem.Service.Models;

namespace TrainBookingSystem.Service.Services
{
    public interface IEmailService
    {
        void sendEmail(Message message);
    }
}
