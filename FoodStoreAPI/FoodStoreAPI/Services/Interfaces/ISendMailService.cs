using FoodStoreAPI.ViewModel.Mail;

namespace FoodStoreAPI.Services.Interfaces
{
    public interface ISendMailService
    {
        Task SendMailAsync(SendMailViewModel mail);
    }
}
