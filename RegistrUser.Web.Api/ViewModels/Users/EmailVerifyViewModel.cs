﻿using RegistrUser.WebApi.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RegistrUser.WebApi.ViewModels.Users
{
    public class EmailVerifyViewModel
    {
        [Required, Email]
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
