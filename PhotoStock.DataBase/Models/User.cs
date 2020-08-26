using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace PhotoStock.DataBase.Models
{
	public class User : IdentityUser
	{
		public List<Photo> Photos;
	}
}
