using Restaurant_Init.Models.EmailModels;

namespace Restaurant_Init.Services.EmailService
{
    public interface IMailService
    {
        Task SendEmailiAsync(MailRequest mailRequest);
    }
}
