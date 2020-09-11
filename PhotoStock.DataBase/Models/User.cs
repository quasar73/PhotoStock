using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PhotoStock.DataBase.Models
{
	public class User : IdentityUser
	{
		public virtual List<Photo> Photos { get; set; }
	}
}
