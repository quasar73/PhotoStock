using PhotoStock.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoStock.Logic.Interfaces
{
	public interface IImageService <TEntity>
	{
		public Task<List<TEntity>> GetImagesAsync(Categories category);
	}
}
