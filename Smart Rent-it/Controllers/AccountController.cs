using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Data;
using Smart_Rent_it.Models;
using Smart_Rent_it.ViewModels; 
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Smart_Rent_it.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Login() => View();

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserEmail", user.Email);
                    HttpContext.Session.SetString("FullName", user.FullName);
                    HttpContext.Session.SetString("UserRole", user.Role); 

                    return RedirectToAction("Index", "Product");
                }

                ViewBag.Error = "Invalid Email or Password!";
            }
            return View(model);
        }


        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ViewBag.Error = "Email already registered!";
                    return View(model);
                }

                var newUser = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    Password = model.Password,
                    Role = "User"
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}