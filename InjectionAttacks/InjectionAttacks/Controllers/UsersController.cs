﻿using Microsoft.AspNetCore.Mvc;

namespace InjectionAttacks.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
