using Microsoft.EntityFrameworkCore;
using RegistrUser.Service.ViewModels.Users;
using RegistrUser.WebApi.Enums;
using RegistrUser.WebApi.Exceptions;
using RegistrUser.WebApi.Extensions;
using RegistrUser.WebApi.Helpers;
using RegistrUser.WebApi.Interfaces.Repositories;
using RegistrUser.WebApi.Interfaces.Services;
using RegistrUser.WebApi.Models;
using RegistrUser.WebApi.Repositories;
using RegistrUser.WebApi.Security;
using RegistrUser.WebApi.Utills;
using RegistrUser.WebApi.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;

namespace RegistrUser.WebApi.Services
{
    public class UserService : IUserService
    {
        
        private readonly IUserRepository _userRepository;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UserService(IUserRepository userRepository, IFileService fileService, 
            IWebHostEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            _fileService = fileService;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var result = await _userRepository.GetAsync(expression);

            if (result is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "RegistrUser not found");

            string webRootPath = _hostingEnvironment.WebRootPath;
            
            var fullPath = webRootPath + "\\" + result.ImagePath;

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            result.ItemState = ItemState.Inactive;

            await _userRepository.UpdateAsync(result);

            await _userRepository.SaveAsync();

            return true;
        }

        public async Task<bool> DeleteAsync()
        {
            var user = await _userRepository.GetAsync(p => p.Id == HttpContextHelper.UserId);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message:"RegistrUser not found");

            string webRootPath = _hostingEnvironment.WebRootPath;

            var fullPath = webRootPath + "\\" + user.ImagePath;

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);

            user.ItemState = ItemState.Inactive;

            await _userRepository.UpdateAsync(user);

            await _userRepository.SaveAsync();

            return true;


        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams? pagination = null, Expression<Func<User, bool>>? expression = null)
        {
            return (from user in _userRepository.GetAllAsync(expression)
                    orderby user.CreatedAt descending
                    select (UserViewModel)user).ToPaged(pagination);
        }

        public async Task<UserViewModel> GetAsync(Expression<Func<User, bool>> expression = null)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "RegistrUser not found");

            return (UserViewModel)user;
        }

        public async Task<bool> UpdateAsync(UserPatchViewModel model)
        {
            var user = await _userRepository.GetAsync(o => o.Id == HttpContextHelper.UserId && o.ItemState == ItemState.Active);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "RegistrUser not found");

            if (model.Image is not null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;

                var fullPath = webRootPath + "\\" + user.ImagePath;

                if (System.IO.File.Exists(fullPath))
                    System.IO.File.Delete(fullPath);

                user.ImagePath = await _fileService.SaveImageAsync(model.Image);
            }
            
            if(model.FirstName is not null)
                user.FirstName = model.FirstName;

            if(model.LastName is not null)
                user.LastName = model.LastName;

            if (model.Email is not null)
            { 
                var email = await _userRepository.GetAsync(o => o.Email == model.Email); 
                
                if (user.Id != email.Id)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Email have already taken");
            }

            if (model.UserName is not null)
            {
                var username = await _userRepository.GetAsync(o => o.UserName == model.UserName);

                if (user.Id != username.Id)
                    throw new StatusCodeException(HttpStatusCode.BadRequest, message: "Username have already taken");

                user.UserName = model.UserName;
            }

            await _userRepository.UpdateAsync(user);

            await _userRepository.SaveAsync();

            return true;
        }

  
    }
}
