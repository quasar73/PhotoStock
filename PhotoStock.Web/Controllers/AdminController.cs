using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Logic.Services;

namespace PhotoStock.Web.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly AdminService adminService;
        public AdminController(AdminService adminService)
        {
            this.adminService = adminService;
        }

        [Route("GetUsers")]
        public async Task<IActionResult> GetUsersList()
        {
            return Ok(await adminService.GetUsersListAsync());
        }
    }
}