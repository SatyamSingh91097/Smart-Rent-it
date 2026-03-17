using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Data;
using Smart_Rent_it.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Smart_Rent_it.Controllers
{
    public class RentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RentController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Create()
        {
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                return RedirectToAction("Login", "Account");
            }

            var userRole = HttpContext.Session.GetString("UserRole");

            if (userRole == "Admin" || userRole == "User")
            {
                return View();
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        public IActionResult Create(Product newProduct, IFormFile ImageFile)
        {
            ModelState.Remove("ImageUrl");
            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(fileStream);
                    }

                    newProduct.ImageUrl = "/images/" + uniqueFileName;
                }
                else if (string.IsNullOrEmpty(newProduct.ImageUrl))
                {
                    newProduct.ImageUrl = "https://picsum.photos/id/50/200/150";
                }

                _context.Products.Add(newProduct);
                _context.SaveChanges();

                return RedirectToAction("Index", "Product");
            }

            return View(newProduct);
        }

        public IActionResult Confirm(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login", "Account");
            }

            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}