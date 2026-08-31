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
                new Product { Id = 2, Brand = "Nike", Name = "Nike Air Max Pulse", ImageUrl = "/images/NIKE+AIR+MAX+PULSE.png", Price = 3980m, StockText = "12 unidades en Bodega Central", Badge = "Nuevo" },
                new Product { Id = 3, Brand = "Adidas", Name = "Adidas Samba OG", ImageUrl = "https://images.unsplash.com/photo-1718220095476-7916e897fc55?q=80&w=1333&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Price = 2950m, StockText = "20 unidades en Bodega Central", Badge = "Popular" },
                new Product { Id = 4, Brand = "New Balance", Name = "New Balance 9060", ImageUrl = "/images/New Balance 9060.png", Price = 4200m, StockText = "9 unidades en Bodega Central", Badge = "Edición limitada" },
                new Product { Id = 5, Brand = "Puma", Name = "Puma RS-X3", ImageUrl = "/images/Puma RS-X3.png", Price = 3290m, StockText = "18 unidades en Bodega Central", Badge = "Trending" },
                new Product { Id = 6, Brand = "ASICS", Name = "ASICS Gel-Kayano 14", ImageUrl = "/images/ASICS Gel-Kayano 14.png", Price = 3860m, StockText = "7 unidades en Bodega Central", Badge = "Colección" }
            };

            return View(products);
        }

        public IActionResult Shirts()
        {
            var products = new List<Product>
            {
                // Camisas Premium y de Lujo
                new Product { Id = 101, Brand = "Gucci", Name = "Gucci Black Logo T-Shirt", ImageUrl = "/images/Gucci_Black Logo _T-Shirt.png", Price = 2890m, StockText = "12 unidades en Bodega Central", Badge = "Lujo" },
                new Product { Id = 102, Brand = "Gucci", Name = "Gucci Nero Embroidered", ImageUrl = "/images/Gucci Nero Embroidered.png", Price = 3200m, StockText = "8 unidades en Bodega Central", Badge = "Exclusivo" },
                new Product { Id = 103, Brand = "Versace", Name = "Versace Medusa Black", ImageUrl = "/images/Versace Medusa Black.png", Price = 3450m, StockText = "10 unidades en Bodega Central", Badge = "Premium" },
                
                // Camisas Deportivas Premium
                new Product { Id = 104, Brand = "Nike", Name = "Nike Dri-FIT Legend", ImageUrl = "/images/Nike Dri-FIT Legend.png", Price = 1290m, StockText = "25 unidades en Bodega Central", Badge = "Top Seller" },
                new Product { Id = 105, Brand = "Adidas", Name = "Adidas Essentials Linear", ImageUrl = "/images/Adidas Essentials Linear.png", Price = 950m, StockText = "30 unidades en Bodega Central", Badge = "Nuevo" },
                new Product { Id = 106, Brand = "Jordan", Name = "Jordan Jumpman Logo Tee", ImageUrl = "/images/Jordan Jumpman Logo Tee.png", Price = 1500m, StockText = "18 unidades en Bodega Central", Badge = "Iconic" },
                
                // Camisas Casuales de Marca
                new Product { Id = 107, Brand = "Puma", Name = "Puma Essentials Black", ImageUrl = "/images/Puma Essentials Black.png", Price = 890m, StockText = "22 unidades en Bodega Central", Badge = "Popular" },
                new Product { Id = 108, Brand = "New Balance", Name = "New Balance Core Tee", ImageUrl = "/images/New Balance Core Tee.png", Price = 1120m, StockText = "16 unidades en Bodega Central", Badge = "Trending" },
                new Product { Id = 109, Brand = "Tommy Hilfiger", Name = "Tommy Hilfiger Flag Logo", ImageUrl = "/images/Tommy Hilfiger Flag Logo.png", Price = 1650m, StockText = "14 unidades en Bodega Central", Badge = "Clásico" },
                
                // Más Opciones Premium
                new Product { Id = 110, Brand = "Dolce & Gabbana", Name = "D&G Black Crown", ImageUrl = "/images/D&G Black Crown.png", Price = 3100m, StockText = "9 unidades en Bodega Central", Badge = "Lujo" },
                new Product { Id = 111, Brand = "Ralph Lauren", Name = "Ralph Lauren Polo Black", ImageUrl = "/images/Ralph Lauren Polo Black.png", Price = 1890m, StockText = "20 unidades en Bodega Central", Badge = "Premium" },
                new Product { Id = 112, Brand = "Calvin Klein", Name = "Calvin Klein Essentials", ImageUrl = "/images/Calvin Klein Essentials.png", Price = 1340m, StockText = "28 unidades en Bodega Central", Badge = "Moderno" }
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
