using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using PhotoStock.DataBase;
using PhotoStock.DataBase.Models;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PhotoStock.Common;
using PhotoStock.Logic.Interfaces;
using PhotoStock.Logic.Services;
using PhotoStock.DataBase.Repositories;
using System.Collections.Generic;
using PhotoStock.Common.ViewModels;

namespace PhotoStock.Web
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(); 
			services.AddControllers();

			services.AddDbContext<ApplicationContext>(options =>
				options.UseLazyLoadingProxies()
				.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
			services.AddIdentity<User, IdentityRole>(opts => {
				opts.Password.RequiredLength = 3;
				opts.Password.RequireNonAlphanumeric = false;
				opts.Password.RequireLowercase = false;
				opts.Password.RequireUppercase = false;
				opts.Password.RequireDigit = false;
			})
				.AddEntityFrameworkStores<ApplicationContext>();
			services.AddAuthorization();

			var jwtSection = Configuration.GetSection("JwtBearerTokenSettings"); 
			services.Configure<JwtBearerTokenSettings>(jwtSection); 
			var jwtBearerTokenSettings = jwtSection.Get<JwtBearerTokenSettings>(); 
			var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidIssuer = jwtBearerTokenSettings.Issuer,
					ValidAudience = jwtBearerTokenSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),
				};
			});
			services.AddTransient<IImportService, PhotoImport>();
			services.AddTransient<IRepository<Photo>, PhotoRepository>();
			services.AddTransient<IImageService<List<PhotoViewModel>>, ImageService>();
			services.AddTransient<IAdminService<UserViewModel>, AdminService>();
			services.AddTransient<IUserRepository<User>, UsersRepository>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(x => x
			   .AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader());

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseStaticFiles();
			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
