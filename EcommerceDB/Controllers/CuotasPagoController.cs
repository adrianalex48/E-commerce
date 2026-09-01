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
    public class CuotasPagoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuotasPagoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CuotasPago
        public async Task<IActionResult> Index()
        {
            return View(await _context.CuotasPago.ToListAsync());
        }

        // GET: CuotasPago/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPago = await _context.CuotasPago
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuotaPago == null)
            {
                return NotFound();
            }

            return View(cuotaPago);
        }

        // GET: CuotasPago/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CuotasPago/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PedidoId,Monto,Fecha,MetodoPago,Estado")] CuotaPago cuotaPago)
        {
            if (!ModelState.IsValid)
            {
                return View(cuotaPago);
            }

            var pedidoExiste = await _context.Pedidos.AnyAsync(p => p.Id == cuotaPago.PedidoId);
            if (!pedidoExiste)
            {
                ModelState.AddModelError(string.Empty, "El pedido indicado no existe.");
                return View(cuotaPago);
            }

            var totalPedido = await _context.DetallesPedido
                .Where(d => d.PedidoId == cuotaPago.PedidoId)
                .SumAsync(d => d.Cantidad * d.PrecioMomentoCompra);

            var pagosPrevios = await _context.CuotasPago
                .Where(c => c.PedidoId == cuotaPago.PedidoId)
                .SumAsync(c => c.Monto);

            var montoTotalComprometido = pagosPrevios + cuotaPago.Monto;
            if (montoTotalComprometido > totalPedido)
            {
                ModelState.AddModelError(
                    string.Empty,
                    $"El pago excede el total calculado del pedido. Total del pedido: {totalPedido:C}. Monto acumulado actual: {montoTotalComprometido:C}.");
                return View(cuotaPago);
            }

            cuotaPago.MetodoPago = "Tarjeta (Simulada)";
            cuotaPago.Estado = string.IsNullOrWhiteSpace(cuotaPago.Estado) ? "Pagado" : cuotaPago.Estado;

            _context.Add(cuotaPago);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CuotasPago/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPago = await _context.CuotasPago.FindAsync(id);
            if (cuotaPago == null)
            {
                return NotFound();
            }
            return View(cuotaPago);
        }

        // POST: CuotasPago/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PedidoId,Monto,Fecha,MetodoPago,Estado")] CuotaPago cuotaPago)
        {
            if (id != cuotaPago.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuotaPago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuotaPagoExists(cuotaPago.Id))
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
            return View(cuotaPago);
        }

        // GET: CuotasPago/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuotaPago = await _context.CuotasPago
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cuotaPago == null)
            {
                return NotFound();
            }

            return View(cuotaPago);
        }

        // POST: CuotasPago/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuotaPago = await _context.CuotasPago.FindAsync(id);
            if (cuotaPago != null)
            {
                _context.CuotasPago.Remove(cuotaPago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuotaPagoExists(int id)
        {
            return _context.CuotasPago.Any(e => e.Id == id);
        }
    }
}
