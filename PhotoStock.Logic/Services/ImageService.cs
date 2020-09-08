using PhotoStock.Common;
using PhotoStock.DataBase.Models;
using PhotoStock.DataBase.Repositories;
using PhotoStock.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoStock.Logic.Services
{
    public class ImageService : IImageService <List<Photo>>
    {
        private readonly IRepository<Photo> repository;
        public ImageService(IRepository<Photo> repository)
        {
            this.repository = repository;
        }
	    public async Task<List<Photo>> GetImagesAsync(Categories category)
		{
            List<Photo> images;
            if (category == Categories.Any)
            {
                images = await repository.GetListAsync();
            }
            else
            {
                images = await repository.GetByCategoryAsync(category);
            }
            return images;
        }
	}
}
