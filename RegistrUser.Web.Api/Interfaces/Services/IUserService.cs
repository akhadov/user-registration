using RegistrUser.Service.ViewModels.Users;
using RegistrUser.WebApi.Models;
using RegistrUser.WebApi.Utills;
using RegistrUser.WebApi.ViewModels.Users;
using System.Linq.Expressions;

namespace RegistrUser.WebApi.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Expression<Func<User, bool>> expression = null!);

        Task<UserViewModel> GetAsync(Expression<Func<User, bool>> expression = null);

        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        
        Task<bool> UpdateAsync(UserPatchViewModel model);

    }
}
