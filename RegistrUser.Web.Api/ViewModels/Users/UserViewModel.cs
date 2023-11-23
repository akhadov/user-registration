﻿using RegistrUser.WebApi.Models;
using System.Text.Json.Serialization;

namespace RegistrUser.WebApi.ViewModels.Users
{
    public class UserViewModel
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = String.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = String.Empty;

        [JsonPropertyName("user_name")]
        public string UserName { get; set; } = String.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = String.Empty;

        [JsonPropertyName("image_path")]
        public string ImagePath { get; set; } = String.Empty;

        public static implicit operator UserViewModel(User user)
        {
            return new UserViewModel
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Id = user.Id,
                ImagePath = user.ImagePath,
            };
        }
    }
}
