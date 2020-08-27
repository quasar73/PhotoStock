using Microsoft.AspNetCore.Http;
using PhotoStock.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoStock.Web.ViewModels
{
	public class ImportViewModel
	{
		[Required]
		public IFormFile File { get; set; }
		[Required]
		public Categories Category { get; set; }
	}
}
