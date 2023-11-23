using RegistrUser.WebApi.ViewModels.Users;
using MimeKit;

namespace RegistrUser.WebApi.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendAsync(EmailMessageViewModel emailMessage);
    }
}
