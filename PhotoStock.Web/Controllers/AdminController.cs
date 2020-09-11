using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Common.ViewModels;
using PhotoStock.Logic.Interfaces;
using PhotoStock.Logic.Services;

namespace PhotoStock.Web.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService<UserViewModel> adminService;
        public AdminController(IAdminService<UserViewModel> adminService)
        {
            this.adminService = adminService;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsersList()
        {
            var users = await adminService.GetUsersListAsync();
            return Ok(users);
        }
    }
}