using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models.ViewModels
{
	public class VerifyEmailViewModel
	{
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }
    }
}

