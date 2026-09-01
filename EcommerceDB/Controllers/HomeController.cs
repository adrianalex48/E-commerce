using EcommerceDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Windbrands.Data;

namespace EcommerceDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? categoriaId)
        {
            var categorias = _context.Categorias.ToList();
            var productosQuery = _context.Productos
                .Include(p => p.Categoria)
                .AsQueryable();

            if (categoriaId.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.CategoriaId == categoriaId.Value);
            }

            var catalogo = new CatalogoViewModel
            {
                Productos = productosQuery.ToList(),
                Categorias = categorias
            };

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(catalogo);
        }

        public IActionResult Shirts()
        {
            var productos = _context.Productos.ToList();
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(productos);
        }

        public IActionResult Privacy()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        public IActionResult Admin()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewBag.Categorias = _context.Categorias.ToList();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
