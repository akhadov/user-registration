﻿using RegistrUser.Service.ViewModels.Users;
using RegistrUser.WebApi.Interfaces.Services;
using RegistrUser.WebApi.Utills;
using RegistrUser.WebApi.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RegistrUser.WebApi.Controllers
{
    [Route("api/accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _acountService;
        private readonly IVerifyEmailService _emailService;

        public AccountsController(IAccountService accountService, IVerifyEmailService emailService)
        {
            _acountService = accountService;
            _emailService = emailService;
        }

        [HttpPost("registr"), AllowAnonymous]
        public async Task<IActionResult> Registr([FromBody]UserCreateViewModel userCreateViewModel)
            => Ok(await _acountService.RegistrAsync(userCreateViewModel));

        [HttpPost("login"), AllowAnonymous]
        public async Task<IActionResult> LogIn([FromBody]UserLogInViewModel logInViewModel)
            => Ok((new { Token = (await _acountService.LogInAsync(logInViewModel))}));

        [HttpPost("verify-email"), AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromBody] EmailVerifyViewModel email)
            => Ok(await _emailService.VerifyEmail(email));

        [HttpPost("send-code-to-email"), AllowAnonymous]
        public async Task<IActionResult> SendToEmail([FromBody] SendCodeToEmailViewModel email)
        {
            await _emailService.SendCodeAsync(email);
            return Ok();
        }

        [HttpPost("reset-password"), AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody]UserResetPasswordViewModel forgotPassword)
        {
            return Ok(await _acountService.VerifyPasswordAsync(forgotPassword));
        }
    }
}
