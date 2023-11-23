﻿using RegistrUser.Service.ViewModels.Users;
using RegistrUser.WebApi.Enums;
using RegistrUser.WebApi.Exceptions;
using RegistrUser.WebApi.Helpers;
using RegistrUser.WebApi.Interfaces.Repositories;
using RegistrUser.WebApi.Interfaces.Services;
using RegistrUser.WebApi.Models;
using RegistrUser.WebApi.Security;
using RegistrUser.WebApi.Utills;
using RegistrUser.WebApi.ViewModels.Users;
using System.Net;

namespace RegistrUser.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _repositroy;
        private readonly IAuthManager _authManager;

        public AccountService(IUserRepository userRepository, IAuthManager authManager)
        {
            _repositroy = userRepository;
            _authManager = authManager;
        }


        public async Task<string?> LogInAsync(UserLogInViewModel viewModel)
        {
            var user = await _repositroy.GetAsync(o => o.Email == viewModel.Email && o.ItemState == ItemState.Active);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "email is wrong");

            if (user.IsEmailConfirmed is false)
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "email did not verified!");

            if (PasswordHasher.Verify(viewModel.Password, user.Salt, user.PasswordHash))
                return _authManager.GenerateToken(user);

            throw new StatusCodeException(HttpStatusCode.BadRequest, message: "password is wrong");
        }

        public async Task<bool> RegistrAsync(UserCreateViewModel viewModel)
        {
            var userk = await _repositroy.GetAsync(o => o.ItemState == ItemState.Active && (o.Email == viewModel.Email || o.UserName == viewModel.UserName));

            if (userk is null)
            {
                var user = (User)viewModel;
                
                var hashResult = PasswordHasher.Hash(viewModel.Password);

                user.Salt = hashResult.Salt;

                user.ItemState = ItemState.Active;

                user.PasswordHash = hashResult.Hash;

                user.CreatedAt = DateTime.UtcNow;

                var result = await _repositroy.CreateAsync(user);

                await _repositroy.SaveAsync();

                throw new StatusCodeException(HttpStatusCode.OK, message: "true");
            }

            throw new StatusCodeException(HttpStatusCode.OK, message: "false");
        }

        public async Task<bool> VerifyPasswordAsync(UserResetPasswordViewModel password)
        {
            var user = await _repositroy.GetAsync(p => p.Email == password.Email && p.ItemState == ItemState.Active);

            if(user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, message: "user not found!");

            if (user.IsEmailConfirmed is false)
                throw new StatusCodeException(HttpStatusCode.BadRequest, message: "email did not verified!");

            var changedPassword = PasswordHasher.ChangePassword(password.Password, user.Salt);

            user.PasswordHash = changedPassword;

            await _repositroy.UpdateAsync(user);
            await _repositroy.SaveAsync();

            throw new StatusCodeException(HttpStatusCode.OK, message: "true");
        }
    }
}