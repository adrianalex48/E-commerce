using EcommerceDB.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EcommerceDB.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Brand = "Jordan", Name = "Air Jordan 1 Retro High OG", ImageUrl = "https://images.unsplash.com/photo-1552346154-21d32810aba3?auto=format&fit=crop&w=400&q=80", Price = 4500m, StockText = "15 unidades en Bodega Central", Badge = "Top Seller" },
                new Product { Id = 2, Brand = "Nike", Name = "Nike Air Max Pulse", ImageUrl = "https://images.unsplash.com/photo-1542291026-7eec264c27ff?auto=format&fit=crop&w=400&q=80", Price = 3980m, StockText = "12 unidades en Bodega Central", Badge = "Nuevo" },
                new Product { Id = 3, Brand = "Adidas", Name = "Adidas Samba OG", ImageUrl = "https://images.unsplash.com/photo-1600185365483-26d7a4cc7519?auto=format&fit=crop&w=400&q=80", Price = 2950m, StockText = "20 unidades en Bodega Central", Badge = "Popular" },
                new Product { Id = 4, Brand = "New Balance", Name = "New Balance 9060", ImageUrl = "https://images.unsplash.com/photo-1608231387042-66d1773070a5?auto=format&fit=crop&w=400&q=80", Price = 4200m, StockText = "9 unidades en Bodega Central", Badge = "Edición limitada" },
                new Product { Id = 5, Brand = "Puma", Name = "Puma RS-X3", ImageUrl = "https://images.unsplash.com/photo-1607522370275-f14206abe5d3?auto=format&fit=crop&w=400&q=80", Price = 3290m, StockText = "18 unidades en Bodega Central", Badge = "Trending" },
                new Product { Id = 6, Brand = "ASICS", Name = "ASICS Gel-Kayano 14", ImageUrl = "https://images.unsplash.com/photo-1525966222134-fcfa99b8ae77?auto=format&fit=crop&w=400&q=80", Price = 3860m, StockText = "7 unidades en Bodega Central", Badge = "Colección" }
            };

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Admin()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
