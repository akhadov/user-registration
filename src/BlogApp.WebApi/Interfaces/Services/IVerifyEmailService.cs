using RegistrUser.Service.ViewModels.Users;
using RegistrUser.WebApi.ViewModels.Users;

namespace RegistrUser.WebApi.Interfaces.Services
{
    public interface IVerifyEmailService
    {
        Task SendCodeAsync(SendCodeToEmailViewModel email);

        Task<bool> VerifyEmail(EmailVerifyViewModel emailVerify);

        Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel model);
    }
}
