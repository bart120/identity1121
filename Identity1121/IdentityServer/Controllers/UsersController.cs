using IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public UsersController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IActionResult> AddUser()
        {
            var u = new User();
            u.UserName = "vleclerc@inow.fr";
            u.Firstname = "Vincent";
            u.Lastname = "Leclerc";
            u.Email = "vleclerc@inow.fr";
            u.EmailConfirmed = true;

            var result = await _userManager.CreateAsync(u, "Toto2021$");
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            else
                return Ok();
        }

        public async Task<IActionResult> AddRole()
        {
            Role r = new Role
            {
                Name = "ADMIN",
                State = 1
            };

            Role r2 = new Role
            {
                Name = "USER",
                State = 1
            };

            await _roleManager.CreateAsync(r);
            await _roleManager.CreateAsync(r2);

            var user = await _userManager.FindByNameAsync("vleclerc@inow.fr");
            await _userManager.AddToRolesAsync(user, new List<string> { "ADMIN", "USER" });

            return Ok();
        }
    }
}
