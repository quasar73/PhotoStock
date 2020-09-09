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
using PhotoStock.DataBase.Repositories;

namespace PhotoStock.Logic.Services
{
	public class AdminService : IAdminService <UserViewModel>
	{
		private readonly IUserRepository<User> repository;
		public AdminService(IUserRepository<User> repository)
		{
			this.repository = repository;
		}
		public async Task<List<UserViewModel>> GetUsersListAsync()
		{
			var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserViewModel>()
			.ForMember(uvm => uvm.NumberOfImports, uvm => uvm.MapFrom(u => u.Photos.Count)));
			return (await repository.GetUsersListAsync()).ProjectTo<UserViewModel>(config).ToList();
		}
	}
}
