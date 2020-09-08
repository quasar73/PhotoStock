using AutoMapper;
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
    public class ImageService : IImageService <PhotoViewModel>
    {
        private readonly IRepository<Photo> repository;
        public ImageService(IRepository<Photo> repository)
        {
            this.repository = repository;
        }
        public async Task<List<PhotoViewModel>> GetImagesAsync(Categories category)
        {
            List<Photo> images = await (category == Categories.Any ? repository.GetListAsync() : repository.GetByCategoryAsync(category));
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Photo, PhotoViewModel>()
            .ForMember(pvm => pvm.UserName, pvm => pvm.MapFrom(p => p.User.UserName)));
            var mapper = new Mapper(config);
            return mapper.Map<List<Photo>, List<PhotoViewModel>>(images);
        }
	}
}
