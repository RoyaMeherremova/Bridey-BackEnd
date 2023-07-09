using BrideyApp.Areas.Admin.ViewModels.Account;
using BrideyApp.Helpers.Enums;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager,
                                             RoleManager<IdentityRole> roleManager,
                                             SignInManager<AppUser> signInManager,
                                             IEmailService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(AdminLoginVM model, string viewName = null, string controllerName = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUsername);

            if (user is null)
            {
                user = await _userManager.FindByNameAsync(model.EmailOrUsername);
            }

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.IsRememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or password is wrong");
                return View(model);
            }
            ViewBag.UserId = await _userManager.FindByNameAsync(model.EmailOrUsername);
            viewName = "Index";
            controllerName = "Dashboard";
            return RedirectToAction("Index", "Dashboard", new { viewName = "Index", controllerName = "Dashboard" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("AdminLogin", "Account");
        }



        [HttpGet]
        public async Task<IActionResult> AddRoleToUser()
        {
            ViewBag.users = await GetUsersAsync();
            ViewBag.roles = await GetRolesAsync();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddRoleToUser(UserRoleVM model)
        {
            ViewBag.users = await GetUsersAsync();
            ViewBag.roles = await GetRolesAsync();

            AppUser user = await _userManager.FindByIdAsync(model.UserId);

            IdentityRole role = await _roleManager.FindByIdAsync(model.RoleId);

            await _userManager.AddToRoleAsync(user, role.Name);

            return View();
        }


        private async Task<SelectList> GetUsersAsync()
        {
            List<AppUser> users = await _userManager.Users.ToListAsync();
            return new SelectList(users, "Id", "FullName");
        }

        private async Task<SelectList> GetRolesAsync()
        {
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();
            return new SelectList(roles, "Id", "Name");
        }

        public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
                }
            }
        }


    }
}
