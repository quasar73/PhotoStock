using System.Linq;
using System.Threading.Tasks;

namespace PhotoStock.DataBase.Repositories
{
	public interface IUserRepository<TEntity>
	{
		public Task<IQueryable<TEntity>> GetUsersListAsync();
	}
}
