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
        private readonly IWebHostEnvironment environment;
        private readonly IImageService<List<Photo>> imageService;
        public PhotoController(UserManager<User> userManager, IImportService importService, IWebHostEnvironment environment, IImageService<List<Photo>> imageService)
        {
            this.userManager = userManager;
            this.importService = importService;
            this.environment = environment;
            this.imageService = imageService;
        }

        [HttpPost]
        [Authorize]
        [Route("Import")]
        public async Task<IActionResult> Import([FromForm] ImportViewModel importVM)
        {
            string userId = userManager.Users.FirstOrDefault(u => u.UserName == User.Identity.Name)?.Id;
            if (ModelState.IsValid && userId != null)
            {
                await importService.ImportPhoto(importVM.File, userId, importVM.Category, environment.WebRootPath);
                return Ok(new { Message = "Photo uploaded successfully!" });
            }
            return new BadRequestObjectResult(new { Message = "Upload was failed"});
        }

        [HttpGet]
        [Route("GetImages")]
        public async Task<IActionResult> GetImages(Categories category)
        {
            var photoList = (await imageService.GetImagesAsync(category)).Select(photo => new PhotoViewModel()
            {
                Path = photo.Path,
                Category = photo.Category,
                UploadDate = photo.UploadDate,
                UserName = photo.User.UserName
            }).ToList();
            return Ok(photoList);
        }

    }
}