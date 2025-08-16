﻿using System;
using LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace LibraryManagementSystem.Data
{
	public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
	

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}

