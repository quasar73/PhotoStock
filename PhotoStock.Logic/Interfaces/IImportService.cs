using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PhotoStock.Common;

namespace PhotoStock.Logic.Interfaces
{
	public interface IImportService
	{
		Task ImportPhoto(IFormFile file, string userId, Categories category, string webrootpath);
	}
}
