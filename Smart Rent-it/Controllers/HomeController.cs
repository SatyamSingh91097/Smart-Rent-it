using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Models;
using System.Diagnostics;

namespace Smart_Rent_it.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Privacy() 
        {
            return View();
        }
    }
}

