using PhotoStock.Common.ViewModels;
using PhotoStock.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStock.Logic.Interfaces
{
	public interface IAdminService <TEntity>
	{
		public Task<List<TEntity>> GetUsersListAsync();
	}
}
