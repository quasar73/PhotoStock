using PhotoStock.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoStock.Web.ViewModels
{
	public class PhotoViewModel
	{
		public string Path { get; set; }
		public string UserName { get; set; }
		public DateTime UploadDate { get; set; }
		public Categories Category { get; set; }
	}
}
