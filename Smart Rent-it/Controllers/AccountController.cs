using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Data;
using Smart_Rent_it.Models;
using Smart_Rent_it.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting; 
using System.Linq;
using System.IO; 
using System;
using System.Threading.Tasks; 

namespace Smart_Rent_it.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment; 

        public AccountController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                    HttpContext.Session.SetString("UserProfilePic", user.ProfilePictureUrl ?? "");
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
                    Role = "User",
                    ProfilePictureUrl = "" 
                };

                _context.Users.Add(newUser);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Profile()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login");
            }

            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null) return NotFound();

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(IFormFile ProfileFile)
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail)) return RedirectToAction("Login");

            var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null) return NotFound();

            if (ProfileFile != null && ProfileFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "profile_pics");
                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfileFile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ProfileFile.CopyToAsync(fileStream);
                }

                if (!string.IsNullOrEmpty(user.ProfilePictureUrl))
                {
                    string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, user.ProfilePictureUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath) && !oldPath.Contains("default-user"))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                user.ProfilePictureUrl = "/profile_pics/" + uniqueFileName;
                _context.SaveChanges();

                HttpContext.Session.SetString("UserProfilePic", user.ProfilePictureUrl);

                TempData["Message"] = "Profile picture updated successfully!";
            }

            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Product");
        }
    }
}