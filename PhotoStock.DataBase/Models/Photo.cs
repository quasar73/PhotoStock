using System;
using System.Collections.Generic;
using System.Text;
using PhotoStock.Common;

namespace PhotoStock.DataBase.Models
{
	public class Photo
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public string UserId { get; set; }
		public virtual User User { get; set; }
		public DateTime UploadDate { get; set; }
		public Categories Category { get; set; }
	}
}
