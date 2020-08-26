﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoStock.DataBase.Models;

namespace PhotoStock.DataBase
{
	class ApplicationContext : IdentityDbContext
	{
		public DbSet<Photo> Photos { get; set; }
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{
			Database.EnsureCreated();
		}
	}
}
