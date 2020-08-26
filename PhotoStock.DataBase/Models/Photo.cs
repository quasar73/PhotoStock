﻿using System;
using System.Collections.Generic;
using System.Text;
using PhotoStock.Common;

namespace PhotoStock.DataBase.Models
{
	public class Photo
	{
		public string Name { get; set; }
		public string Path { get; set; }
		public string UserId { get; set; }
		public DateTime UploadDate { get; set; }
		public Categories.Type Category { get; set; }
	}
}
