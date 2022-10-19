namespace RegistrUser.WebApi.Interfaces.Services
{
    public interface IFileService
    {
        Task<string> SaveImageAsync(IFormFile image);
    }
}
