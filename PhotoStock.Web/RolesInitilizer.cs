using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace PhotoStock.Web
{
	public class RolesInitilizer
	{
		public static async Task InitilizeAsync(RoleManager<IdentityRole> roleManager)
		{
			if (await roleManager.FindByNameAsync("admin") == null)
				await roleManager.CreateAsync(new IdentityRole("admin"));
			if (await roleManager.FindByNameAsync("user") == null)
				await roleManager.CreateAsync(new IdentityRole("user"));
		}
	}
}
