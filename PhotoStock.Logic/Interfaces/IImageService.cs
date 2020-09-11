﻿using PhotoStock.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoStock.Logic.Interfaces
{
	public interface IImageService <TResult>
	{
		public Task<TResult> GetImagesAsync(Categories category);
	}
}
