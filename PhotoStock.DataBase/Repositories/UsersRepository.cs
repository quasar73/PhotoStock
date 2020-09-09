using PhotoStock.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStock.DataBase.Repositories
{
	public class UsersRepository : IUserRepository<User>
	{
		private readonly ApplicationContext context;
		public UsersRepository(ApplicationContext context)
		{
			this.context = context;
		}
		public async Task<IQueryable<User>> GetUsersListAsync()
		{
			return context.Users.AsQueryable();
		}
	}
}
