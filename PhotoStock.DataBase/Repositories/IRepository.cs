using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhotoStock.DataBase.Repositories
{
	public interface IRepository<TEntity>
	{
		Task<List<TEntity>> GetListAsync();
		Task CreateAsync(TEntity entity);
		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(TEntity entity);
		Task<TEntity> GetByIdAsync(int id);
	}
}
