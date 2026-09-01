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
    public class ProveedorProductosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProveedorProductosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ProveedorProductos
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProveedoresProductos.ToListAsync());
        }

        // GET: ProveedorProductos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorProducto = await _context.ProveedoresProductos
                .FirstOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedorProducto == null)
            {
                return NotFound();
            }

            return View(proveedorProducto);
        }

        // GET: ProveedorProductos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProveedorProductos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProveedorId,ProductoId,PrecioCompra,TiempoEntregaDias")] ProveedorProducto proveedorProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(proveedorProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(proveedorProducto);
        }

        // GET: ProveedorProductos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorProducto = await _context.ProveedoresProductos.FindAsync(id);
            if (proveedorProducto == null)
            {
                return NotFound();
            }
            return View(proveedorProducto);
        }

        // POST: ProveedorProductos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProveedorId,ProductoId,PrecioCompra,TiempoEntregaDias")] ProveedorProducto proveedorProducto)
        {
            if (id != proveedorProducto.ProveedorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(proveedorProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProveedorProductoExists(proveedorProducto.ProveedorId))
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
            return View(proveedorProducto);
        }

        // GET: ProveedorProductos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var proveedorProducto = await _context.ProveedoresProductos
                .FirstOrDefaultAsync(m => m.ProveedorId == id);
            if (proveedorProducto == null)
            {
                return NotFound();
            }

            return View(proveedorProducto);
        }

        // POST: ProveedorProductos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var proveedorProducto = await _context.ProveedoresProductos.FindAsync(id);
            if (proveedorProducto != null)
            {
                _context.ProveedoresProductos.Remove(proveedorProducto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProveedorProductoExists(int id)
        {
            return _context.ProveedoresProductos.Any(e => e.ProveedorId == id);
        }
    }
}
