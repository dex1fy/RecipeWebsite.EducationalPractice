﻿using System.ComponentModel.DataAnnotations;

namespace RecipeWebsiteBackend.Models.DTOs.Registration
{
    public class RegisterRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserName { get; set; }

    }
}
