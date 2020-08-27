using Microsoft.AspNetCore.Http;
using PhotoStock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoStock.Web.ViewModels
{
	public class ImportViewModel
	{
		public IFormFile File { get; set; }
		public Categories Category { get; set; }
	}
}
