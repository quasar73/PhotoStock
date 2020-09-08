using PhotoStock.Common;
using PhotoStock.Common.ViewModels;
using PhotoStock.DataBase.Models;
using PhotoStock.DataBase.Repositories;
using PhotoStock.Logic.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoStock.Logic.Services
{
    public class ImageService : IImageService <List<PhotoViewModel>>
    {
        private readonly IRepository<Photo> repository;
        public ImageService(IRepository<Photo> repository)
        {
            this.repository = repository;
        }
        public async Task<List<PhotoViewModel>> GetImagesAsync(Categories category)
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
            return images.Select(photo => new PhotoViewModel()
            {
                Path = photo.Path,
                Category = photo.Category,
                UploadDate = photo.UploadDate,
                UserName = photo.User.UserName
            }).ToList(); ;
        }
	}
}
