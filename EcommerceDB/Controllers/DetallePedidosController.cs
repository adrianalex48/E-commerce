using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EcommerceDB.Models;
using Windbrands.Data;

namespace EcommerceDB.Controllers
{
    public class DetallePedidosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DetallePedidosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DetallePedidos
        public async Task<IActionResult> Index()
        {
            return View(await _context.DetallesPedido.ToListAsync());
        }

        // GET: DetallePedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallesPedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // GET: DetallePedidos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DetallePedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PedidoId,ProductoId,Cantidad,PrecioMomentoCompra")] DetallePedido detallePedido)
        {
            if (!ModelState.IsValid)
            {
                return View(detallePedido);
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(p => p.Id == detallePedido.PedidoId);

            if (pedido == null)
            {
                ModelState.AddModelError(string.Empty, "El pedido indicado no existe.");
                return View(detallePedido);
            }

            var stock = await _context.StockBodegas
                .FirstOrDefaultAsync(s => s.ProductoId == detallePedido.ProductoId && s.BodegaId == pedido.BodegaOrigenId);

            if (stock == null)
            {
                ModelState.AddModelError(string.Empty, "No existe stock para ese producto en la bodega de origen.");
                return View(detallePedido);
            }

            if (stock.Cantidad < detallePedido.Cantidad)
            {
                ModelState.AddModelError(string.Empty, $"No hay suficiente inventario. Stock disponible: {stock.Cantidad}, cantidad solicitada: {detallePedido.Cantidad}.");
                return View(detallePedido);
            }

            stock.Cantidad -= detallePedido.Cantidad;
            _context.Add(detallePedido);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: DetallePedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallesPedido.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PedidoId,ProductoId,Cantidad,PrecioMomentoCompra")] DetallePedido detallePedido)
        {
            if (id != detallePedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detallePedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetallePedidoExists(detallePedido.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(detallePedido);
        }

        // GET: DetallePedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detallePedido = await _context.DetallesPedido
                .FirstOrDefaultAsync(m => m.Id == id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            return View(detallePedido);
        }

        // POST: DetallePedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detallePedido = await _context.DetallesPedido.FindAsync(id);
            if (detallePedido != null)
            {
                _context.DetallesPedido.Remove(detallePedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetallePedidoExists(int id)
        {
            return _context.DetallesPedido.Any(e => e.Id == id);
        }
    }
}
