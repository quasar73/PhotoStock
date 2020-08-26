using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoStock.DataBase.Models
{
	enum Categories
	{
		Nature,
		Animals,
		Human,
		Weapon,
		Thing,
		Other
	}
	class Photo
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public string UserId { get; set; }
		public DateTime UploadDate { get; set; }
		public Categories Category { get; set; }
	}
}
