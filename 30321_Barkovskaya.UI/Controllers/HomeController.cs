﻿using Microsoft.AspNetCore.Mvc;

namespace _30321_Barkovskaya.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
