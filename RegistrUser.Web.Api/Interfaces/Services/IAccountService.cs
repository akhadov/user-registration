using RegistrUser.Service.ViewModels.Users;
using RegistrUser.WebApi.Utills;
using RegistrUser.WebApi.ViewModels.Users;

namespace RegistrUser.WebApi.Interfaces.Services
{
    public interface IAccountService
    {
        Task<bool> RegistrAsync(UserCreateViewModel model);

        Task<string?> LogInAsync(UserLogInViewModel model);


        Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel password);
    }
}
