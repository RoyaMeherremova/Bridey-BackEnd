﻿using BrideyApp.Areas.Admin.ViewModels.User;
using BrideyApp.Data;
using BrideyApp.Helpers;
using BrideyApp.Models;
using BrideyApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly AppDbContext _context;
        public UserController(IUserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            List<AppUser> appUsers = await _userService.GetPaginatedDatasAsync(page, take);
            List<UserVM> mappedDatas = GetDatas(appUsers);

            int pageCount = await GetPageCountAsync(take);

            Paginate<UserVM> paginatedDatas = new(mappedDatas, page, pageCount);

            return View(paginatedDatas);
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            var userCount = await _userService.GetCountAsync();
            return (int)Math.Ceiling((decimal)userCount / take);
        }
        private List<UserVM> GetDatas(List<AppUser> users)
        {
            List<UserVM> mappedDatas = new();
            foreach (var user in users)
            {
                UserVM userList = new()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    UserName = user.UserName
                };
                mappedDatas.Add(userList);
            }
            return mappedDatas;
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                if (id == null) return BadRequest();

                AppUser user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

                if (user is null) return NotFound();

                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                ViewBag.error = ex.Message;
                return View();
            }
        }
    }
}
