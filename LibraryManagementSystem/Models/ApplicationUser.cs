﻿using System;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Models
{
	public class ApplicationUser : IdentityUser
    {
		public string FullName { get; set; }
	}
}

