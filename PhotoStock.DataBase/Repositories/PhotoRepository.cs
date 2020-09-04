using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PhotoStock.DataBase.Models;

namespace PhotoStock.DataBase.Repositories
{
	public class PhotoRepository : IRepository<Photo>
	{
		private readonly ApplicationContext context;
		public PhotoRepository(ApplicationContext context)
		{
			this.context = context;
		}
		public async Task CreateAsync(Photo entity)
		{
			await context.Photos.AddAsync(entity);
			await context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Photo entity)
		{
			context.Photos.Remove(entity);
			await context.SaveChangesAsync();
		}

		public async Task<Photo> GetByIdAsync(int id)
		{
			return await context.Photos.FirstOrDefaultAsync<Photo>(p => p.Id == id);
		}

		public async Task<List<Photo>> GetListAsync()
		{
			return await context.Photos.ToListAsync<Photo>();
		}

		public async Task UpdateAsync(Photo entity)
		{
			var target = context.Photos.FirstOrDefault(p => p.Id == entity.Id);
			if(entity != null)
			{
				target.Name = entity.Name;
				target.Path = entity.Path;
				target.UploadDate = entity.UploadDate;
				target.Category = entity.Category;
				context.Photos.Update(target);
				await context.SaveChangesAsync();
			}
		}
	}
}
