using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeaShopMVC.ViewModel;

namespace TeaShopMVC.Controllers
{
    [Authorize(Roles = "admin")]
    //Admin _Boss2023
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<IdentityUser> userManager1, RoleManager<IdentityRole> roleManager1)
        {
            userManager = userManager1;
            roleManager = roleManager1;
        }
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }
        public async Task<IActionResult> DeleteUserAsync(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> EditUserRole(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var roles = roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    AllRoles = roles,
                    UserRoles = userRoles
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]

        public async Task<IActionResult> EditUserRole(string id, List<string> roles)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removeRoles = userRoles.Except(roles);
                await userManager.RemoveFromRolesAsync(user, removeRoles);
                await userManager.AddToRolesAsync(user, addedRoles);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult GetRoles()
        {
            return View(roleManager.Roles.ToList());
        }

        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
        }
        public async Task<IActionResult> DeleteRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role != null) return View(role);
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteRoleAsync(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
                return NotFound();
            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded) return RedirectToAction("GetRoles");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        public async Task<IActionResult> RenameRole(string Id)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role != null)
                return View(role);
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> RenameRole(string Id, string name)
        {
            var role = await roleManager.FindByIdAsync(Id);
            if (role == null)
                return NotFound();
            if (!string.IsNullOrEmpty(name))
            {
                var result = await roleManager.SetRoleNameAsync(role, name);
                await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("GetRoles");

            }
            return View(role);

        }
    }
}
