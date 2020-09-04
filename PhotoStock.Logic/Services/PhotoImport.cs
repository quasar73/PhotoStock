using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhotoStock.Logic.Interfaces;
using PhotoStock.DataBase.Models;
using PhotoStock.Common;
using PhotoStock.DataBase.Repositories;
using PhotoStock.DataBase;
using ImageMagick;

namespace PhotoStock.Logic.Services
{
	public class PhotoImport : IImportService
	{
		private readonly PhotoRepository repository;
		public PhotoImport(ApplicationContext contex)
		{
			repository = new PhotoRepository(contex);
		}
		public async Task ImportPhoto(IFormFile file, string userId, Categories category, string webrootpath)
		{
			if(file != null)
			{
				string path = "Photos/" + file.FileName;
				using(var fileStream = new FileStream(path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
				var optimizer = new ImageOptimizer();
				optimizer.Compress(webrootpath);
				Photo photo = new Photo()
				{
					Name = Guid.NewGuid().ToString(),
					UserId = userId,
					UploadDate = DateTime.Now,
					Path = path,
					Category = category
				};
				await repository.CreateAsync(photo);
			}
		}
	}
}
