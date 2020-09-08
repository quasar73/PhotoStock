using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PhotoStock.Common;
using PhotoStock.DataBase.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotoStock.Common.ViewModels;

namespace PhotoStock.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtBearerTokenSettings jwtBearerTokenSettings;
        private readonly UserManager<User> userManager;

        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, UserManager<User> userManager)
        {
            this.jwtBearerTokenSettings = jwtTokenOptions.Value;
            this.userManager = userManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]RegistrationViewModel regVM)
        {
            if (!ModelState.IsValid || regVM == null)
                return new BadRequestObjectResult(new { Message = "User Registration Failed" });
            var user = new User() { UserName = regVM.UserName, Email = regVM.Email };
            var result = await userManager.CreateAsync(user, regVM.Password);
            if (!result.Succeeded)
            {
                var dictionary = new ModelStateDictionary();
                foreach (IdentityError error in result.Errors)
                    dictionary.AddModelError(error.Code, error.Description);
                return new BadRequestObjectResult(new { Message = "User Registration Failed", Errors = dictionary });
            }
            return Ok(new { Message = "User Reigstration Successful" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginVM)
        {
            User user;
            if (!ModelState.IsValid || loginVM == null || (user = (User)await ValidateUser(loginVM)) == null)
                return new BadRequestObjectResult(new { Message = "Login failed" });
            var token = GenerateToken(user);
            return Ok(new { Token = token, Message = "Success" });
        }

        private async Task<IdentityUser> ValidateUser(LoginViewModel loginVM)
        {
            var user = await userManager.FindByNameAsync(loginVM.Username);
            if (user != null)
            {
                var result = userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, loginVM.Password);
                return result == PasswordVerificationResult.Failed ? null : user;
            }
            return null;
        }


        private object GenerateToken(User identityUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtBearerTokenSettings.SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, identityUser.UserName.ToString()),
                    new Claim(ClaimTypes.Email, identityUser.Email)
                }),
                Expires = DateTime.UtcNow.AddSeconds(jwtBearerTokenSettings.ExpiryTimeInSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = jwtBearerTokenSettings.Audience,
                Issuer = jwtBearerTokenSettings.Issuer,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}