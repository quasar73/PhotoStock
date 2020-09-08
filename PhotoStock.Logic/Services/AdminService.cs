using PhotoStock.DataBase;
using PhotoStock.DataBase.Models;
using PhotoStock.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PhotoStock.Common.ViewModels;
using System.Linq;
using AutoMapper.QueryableExtensions;

namespace PhotoStock.Logic.Services
{
	public class AdminService : IAdminService <UserViewModel>
	{
		private readonly ApplicationContext context;
		public AdminService(ApplicationContext context)
		{
			this.context = context;
		}
		public async Task<List<UserViewModel>> GetUsersListAsync()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>()
			.ForMember(uvm => uvm.NumberOfImports, uvm => uvm.MapFrom(u => u.Photos.Count)));
			return context.Users.ProjectTo<UserViewModel>(config).ToList();
		}
	}
}
