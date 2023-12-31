﻿using System.ComponentModel.DataAnnotations;

namespace FoodStoreAPI.ViewModel.User
{
    public class RegisterViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
        public string? UserName { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        public RegisterViewModel()
        {
            this.UserName = this.Email;
        }
    }
}
