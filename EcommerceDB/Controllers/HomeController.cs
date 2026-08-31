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
                new Product { Id = 3, Brand = "Adidas", Name = "Adidas Samba OG", ImageUrl = "https://images.unsplash.com/photo-1718220095476-7916e897fc55?q=80&w=1333&auto=format&fit=crop&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D", Price = 2950m, StockText = "20 unidades en Bodega Central", Badge = "Popular" },
                new Product { Id = 4, Brand = "New Balance", Name = "New Balance 9060", ImageUrl = "https://images.unsplash.com/photo-1608231387042-66d1773070a5?auto=format&fit=crop&w=400&q=80", Price = 4200m, StockText = "9 unidades en Bodega Central", Badge = "Edición limitada" },
                new Product { Id = 5, Brand = "Puma", Name = "Puma RS-X3", ImageUrl = "https://images.unsplash.com/photo-1607522370275-f14206abe5d3?auto=format&fit=crop&w=400&q=80", Price = 3290m, StockText = "18 unidades en Bodega Central", Badge = "Trending" },
                new Product { Id = 6, Brand = "ASICS", Name = "ASICS Gel-Kayano 14", ImageUrl = "https://images.unsplash.com/photo-1525966222134-fcfa99b8ae77?auto=format&fit=crop&w=400&q=80", Price = 3860m, StockText = "7 unidades en Bodega Central", Badge = "Colección" }
            };

            return View(products);
        }

        public IActionResult Shirts()
        {
            var products = new List<Product>
            {
                // Camisas Premium y de Lujo
                new Product { Id = 101, Brand = "Gucci", Name = "Gucci Black Logo T-Shirt", ImageUrl = "https://images.unsplash.com/photo-1598938750952-b7a52fb28338?auto=format&fit=crop&w=500&q=90", Price = 2890m, StockText = "12 unidades en Bodega Central", Badge = "Lujo" },
                new Product { Id = 102, Brand = "Gucci", Name = "Gucci Nero Embroidered", ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=500&q=90", Price = 3200m, StockText = "8 unidades en Bodega Central", Badge = "Exclusivo" },
                new Product { Id = 103, Brand = "Versace", Name = "Versace Medusa Black", ImageUrl = "https://images.unsplash.com/photo-1503342394128-c104cbb3b4e3?auto=format&fit=crop&w=500&q=90", Price = 3450m, StockText = "10 unidades en Bodega Central", Badge = "Premium" },
                
                // Camisas Deportivas Premium
                new Product { Id = 104, Brand = "Nike", Name = "Nike Dri-FIT Legend", ImageUrl = "https://images.unsplash.com/photo-1604328698692-f76ea0ae2fa1?auto=format&fit=crop&w=500&q=90", Price = 1290m, StockText = "25 unidades en Bodega Central", Badge = "Top Seller" },
                new Product { Id = 105, Brand = "Adidas", Name = "Adidas Essentials Linear", ImageUrl = "https://images.unsplash.com/photo-1556821552-5f8c3e39d1b8?auto=format&fit=crop&w=500&q=90", Price = 950m, StockText = "30 unidades en Bodega Central", Badge = "Nuevo" },
                new Product { Id = 106, Brand = "Jordan", Name = "Jordan Jumpman Logo Tee", ImageUrl = "https://images.unsplash.com/photo-1554521726-c5343a92e7e2?auto=format&fit=crop&w=500&q=90", Price = 1500m, StockText = "18 unidades en Bodega Central", Badge = "Iconic" },
                
                // Camisas Casuales de Marca
                new Product { Id = 107, Brand = "Puma", Name = "Puma Essentials Black", ImageUrl = "https://images.unsplash.com/photo-1460556290160-1b76a47440f1?auto=format&fit=crop&w=500&q=90", Price = 890m, StockText = "22 unidades en Bodega Central", Badge = "Popular" },
                new Product { Id = 108, Brand = "New Balance", Name = "New Balance Core Tee", ImageUrl = "https://images.unsplash.com/photo-1527689377591-44a9c1d4b8e5?auto=format&fit=crop&w=500&q=90", Price = 1120m, StockText = "16 unidades en Bodega Central", Badge = "Trending" },
                new Product { Id = 109, Brand = "Tommy Hilfiger", Name = "Tommy Hilfiger Flag Logo", ImageUrl = "https://images.unsplash.com/photo-1508427953056-b3fb776b464d?auto=format&fit=crop&w=500&q=90", Price = 1650m, StockText = "14 unidades en Bodega Central", Badge = "Clásico" },
                
                // Más Opciones Premium
                new Product { Id = 110, Brand = "Dolce & Gabbana", Name = "D&G Black Crown", ImageUrl = "https://images.unsplash.com/photo-1489749798305-4fea3ba63d60?auto=format&fit=crop&w=500&q=90", Price = 3100m, StockText = "9 unidades en Bodega Central", Badge = "Lujo" },
                new Product { Id = 111, Brand = "Ralph Lauren", Name = "Ralph Lauren Polo Black", ImageUrl = "https://images.unsplash.com/photo-1491553895911-0055eca6402d?auto=format&fit=crop&w=500&q=90", Price = 1890m, StockText = "20 unidades en Bodega Central", Badge = "Premium" },
                new Product { Id = 112, Brand = "Calvin Klein", Name = "Calvin Klein Essentials", ImageUrl = "https://images.unsplash.com/photo-1521572163474-6864f9cf17ab?auto=format&fit=crop&w=500&q=90", Price = 1340m, StockText = "28 unidades en Bodega Central", Badge = "Moderno" }
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
