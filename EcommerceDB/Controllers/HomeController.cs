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

        public IActionResult Index(int? categoriaId, string busqueda)
        {
            var categorias = _context.Categorias.ToList();
            var productosQuery = _context.Productos
                .Include(p => p.Categoria)
                .AsQueryable();

            if (categoriaId.HasValue)
            {
                productosQuery = productosQuery.Where(p => p.CategoriaId == categoriaId.Value);
            }

            if (!string.IsNullOrEmpty(busqueda))
            {
                productosQuery = productosQuery.Where(p => p.Nombre.Contains(busqueda));
            }

            var catalogo = new CatalogoViewModel
            {
                Productos = productosQuery.ToList(),
                Categorias = categorias
            };

            ViewBag.Categorias = _context.Categorias.ToList();
            ViewBag.BusquedaActual = busqueda;
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

        // GET: Mostrar formulario de compra
        public IActionResult ComprarProducto(int productoid)
        {
            var producto = _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == productoid);

            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.Categorias = _context.Categorias.ToList();
            return View(producto);
        }

        // POST: Procesar compra con tarjeta simulada
        [HttpPost]
        public IActionResult ComprarProducto(int productoid, string nombreCliente, string correo, string numeroTarjeta, string expiracion, string cvv, int cantidad)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.Id == productoid);

            if (producto == null)
            {
                return NotFound();
            }

            // Validar tarjeta de crédito (simulado)
            if (string.IsNullOrEmpty(numeroTarjeta) || numeroTarjeta.Length != 16 || !long.TryParse(numeroTarjeta, out _))
            {
                ModelState.AddModelError("", "Número de tarjeta inválido (debe ser 16 dígitos)");
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            if (cantidad <= 0)
            {
                ModelState.AddModelError("", "La cantidad debe ser mayor a 0");
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }

            try
            {
                // Verificar que la bodega exista, si no, crearla
                var bodega = _context.Bodegas.FirstOrDefault(b => b.Id == 1);
                if (bodega == null)
                {
                    bodega = new Bodega
                    {
                        Ubicacion = "Bodega Central",
                        Capacidad = 1000
                    };
                    _context.Bodegas.Add(bodega);
                    _context.SaveChanges();
                }

                // Crear o buscar cliente
                var cliente = _context.Clientes.FirstOrDefault(c => c.Correo == correo);
                if (cliente == null)
                {
                    cliente = new Cliente
                    {
                        NombreCompleto = nombreCliente,
                        Correo = correo
                    };
                    _context.Clientes.Add(cliente);
                    _context.SaveChanges();
                }

                // Crear pedido
                var pedido = new Pedido
                {
                    ClienteId = cliente.Id,
                    BodegaOrigenId = 1, // Bodega por defecto
                    FechaHora = DateTime.Now,
                    Estado = "Completado"
                };
                _context.Pedidos.Add(pedido);
                _context.SaveChanges();

                // Crear detalle del pedido
                var detallePedido = new DetallePedido
                {
                    PedidoId = pedido.Id,
                    ProductoId = productoid,
                    Cantidad = cantidad,
                    PrecioMomentoCompra = producto.PrecioVenta
                };
                _context.DetallesPedido.Add(detallePedido);
                _context.SaveChanges();

                // Redirigir a confirmación
                TempData["MensajeExito"] = $"¡Compra realizada! Pedido #{pedido.Id} confirmado.";
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbEx)
            {
                ModelState.AddModelError("", "Error en la base de datos: " + dbEx.InnerException?.Message);
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al procesar la compra: " + ex.InnerException?.Message ?? ex.Message);
                ViewBag.Categorias = _context.Categorias.ToList();
                return View(producto);
            }
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
