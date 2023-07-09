﻿using BrideyApp.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BrideyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index(string viewName, string controllerName)
        {
            if (viewName == "Index" && controllerName == "Dashboard")
            {
                return View();
            }
            return RedirectToAction("AdminLogin", "Account");
        }
    }
}
