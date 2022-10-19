using RegistrUser.WebApi.Models;

namespace RegistrUser.WebApi.Interfaces.Services
{
    public interface IAuthManager
    {
        public string GenerateToken(User user);
    }
}
