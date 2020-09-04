using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Common;
using PhotoStock.DataBase;
using PhotoStock.DataBase.Models;
using PhotoStock.DataBase.Repositories;
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
        private readonly PhotoRepository repository;
        private readonly ApplicationContext context;
        public PhotoController(UserManager<User> userManager, IImportService importService, IWebHostEnvironment env, ApplicationContext context)
        {
            this.userManager = userManager;
            this.importService = importService;
            this.env = env;
            this.context = context;
            repository = new PhotoRepository(context);
        }

        [HttpPost]
        [Authorize]
        [Route("Import")]
        public async Task<IActionResult> Import([FromForm] ImportViewModel importVM)
        {
            string userId = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (ModelState.IsValid && userId != null)
            {
                await importService.ImportPhoto(importVM.File, userId, importVM.Category, env.WebRootPath);
                return Ok(new { Message = "Photo uploaded successfully!" });
            }
            return new BadRequestObjectResult(new { Message = "Upload was failed"});
        }

        [HttpGet]
        [Route("GetImages")]
        public async Task<IActionResult> GetImages()
        {
            var images = repository.GetListAsync().Result;
            var photoList = images.Select(photo => new PhotoViewModel() {
                Path = photo.Path,
                Category = photo.Category,
                UploadDate = photo.UploadDate,
                UserName = context.Users.FirstOrDefault(u => u.Id == photo.UserId).UserName
            }).ToList();
            return Ok(photoList);
        }
    }
}