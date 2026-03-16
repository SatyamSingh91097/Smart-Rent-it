using Microsoft.AspNetCore.Mvc;
using Smart_Rent_it.Models;
using System.Collections.Generic;
using System.Linq;

namespace Smart_Rent_it.Controllers
{
    public class ProductController : Controller
    {
        public static List<Product> _products = new List<Product>
        {
            new Product {
                Id = 1,
                Name = "DSLR Camera",
                Description = "Canon EOS R5 for professional photography.",
                PricePerDay = 1500,
                ImageUrl = "https://images.unsplash.com/photo-1516035069371-29a1b244cc32?w=400&h=300&fit=crop",
                IsAvailable = true
            },
            new Product {
                Id = 2,
                Name = "Camping Tent",
                Description = "4-person waterproof tent for mountains.",
                PricePerDay = 500,
                ImageUrl = "https://images.unsplash.com/photo-1504280390367-361c6d9f38f4?w=400&h=300&fit=crop",
                IsAvailable = true
            },
            new Product {
                Id = 3,
                Name = "Electric Drill",
                Description = "High-power drill for home improvement.",
                PricePerDay = 300,
                ImageUrl = "https://images.unsplash.com/photo-1504148455328-c376907d081c?w=400&h=300&fit=crop",
                IsAvailable = false
            },
            new Product {
                Id = 4,
                Name = "Mountain Bike",
                Description = "Sturdy 21-gear bike for off-road trails.",
                PricePerDay = 800,
                ImageUrl = "https://images.unsplash.com/photo-1485965120184-e220f721d03e?w=400&h=300&fit=crop",
                IsAvailable = true
            },
            new Product {
                Id = 5,
                Name = "Gaming Laptop",
                Description = "RTX 4060, 16GB RAM for high-end gaming.",
                PricePerDay = 2500,
                ImageUrl = "https://images.unsplash.com/photo-1593642702821-c8da6771f0c6?w=400&h=300&fit=crop",
                IsAvailable = true
            },
            new Product {
                Id = 6,
                Name = "Acoustic Guitar",
                Description = "Premium wooden guitar for music lovers.",
                PricePerDay = 400,
                ImageUrl = "https://images.unsplash.com/photo-1510915361894-db8b60106cb1?w=400&h=300&fit=crop",
                IsAvailable = true
            },
            new Product {
    Id = 7,
    Name = "Professional Mountain Bike",
    Description = "21-speed gears, perfect for off-road adventure and trekking.",
    PricePerDay = 800,
    ImageUrl = "https://images.pexels.com/photos/100582/pexels-photo-100582.jpeg?auto=compress&cs=tinysrgb&w=400&h=300&fit=crop",
    IsAvailable = true
},
            new Product {
                Id = 8,
                Name = "Electric Scooter",
                Description = "Eco-friendly city commuter with 25km range.",
                PricePerDay = 600,
                ImageUrl = "https://images.unsplash.com/photo-1558981285-6f0c94958bb6?w=400&h=300&fit=crop",
                IsAvailable = true
            },
            new Product {
                Id = 9,
                Name = "Drone with 4K Camera",
                Description = "DJI Mavic Mini for stunning aerial shots.",
                PricePerDay = 2000,
                ImageUrl = "https://images.unsplash.com/photo-1507582020474-9a35b7d455d9?w=400&h=300&fit=crop",
                IsAvailable = true
            },
       new Product {
    Id = 10,
    Name = "Gaming Headset",
    Description = "7.1 Surround sound with noise-canceling mic for pro gamers.",
    PricePerDay = 250,
    ImageUrl = "https://images.pexels.com/photos/1649771/pexels-photo-1649771.jpeg?auto=compress&cs=tinysrgb&w=400&h=300&fit=crop",
    IsAvailable = true
}
        }; // <--- Yahan semicolon miss tha!

        public IActionResult Index() => View(_products);

        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product == null ? NotFound() : View(product);
        }
    }
}