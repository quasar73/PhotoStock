using System.ComponentModel.DataAnnotations;
namespace PhotoStock.Web.ViewModels
{
	public class RegistrationViewModel
	{
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
