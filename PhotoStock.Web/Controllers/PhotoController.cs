using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.DataBase.Models;
using PhotoStock.Logic.Interfaces;
using PhotoStock.Web.ViewModels;

namespace PhotoStock.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IImportService importService;
        private readonly IWebHostEnvironment env;
        public PhotoController(UserManager<User> userManager, IImportService importService, IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.importService = importService;
            this.env = env;
        }

        [HttpPost]
        [Authorize]
        [Route("Import")]
        public async Task<IActionResult> Import([FromForm] ImportViewModel importVM)
        {
            string userId = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (importVM != null && userId != null && importVM.File != null)
            {
                await importService.ImportPhoto(importVM.File, userId, importVM.Category, env.WebRootPath);
                return Ok(new { Message = "Photo uploaded successfully!" });
            }
            return new BadRequestObjectResult(new { Message = "Upload was failed"});
        }
    }
}