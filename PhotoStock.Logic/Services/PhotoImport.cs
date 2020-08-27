using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PhotoStock.Logic.Interfaces;
using PhotoStock.DataBase.Models;
using PhotoStock.Common;
using PhotoStock.DataBase.Repositories;
using PhotoStock.DataBase;

namespace PhotoStock.Logic.Services
{
	class PhotoImport : IImportService
	{
		private readonly IWebHostEnvironment appEnvironment;
		private readonly PhotoRepository repository;
		public PhotoImport(IWebHostEnvironment appEnvironment, ApplicationContext contex)
		{
			this.appEnvironment = appEnvironment;
			repository = new PhotoRepository(contex);
		}
		public async Task ImportPhoto(IFormFile file, string userId, Categories category)
		{
			if(file != null)
			{
				string path = "/Photos/" + file.FileName;
				using(var fileStream = new FileStream(appEnvironment.WebRootPath + path, FileMode.Create))
				{
					await file.CopyToAsync(fileStream);
				}
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
