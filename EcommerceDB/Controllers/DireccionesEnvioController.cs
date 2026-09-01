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
    public class DireccionesEnvioController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DireccionesEnvioController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DireccionesEnvio
        public async Task<IActionResult> Index()
        {
            return View(await _context.DireccionesEnvio.ToListAsync());
        }

        // GET: DireccionesEnvio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _context.DireccionesEnvio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionEnvio == null)
            {
                return NotFound();
            }

            return View(direccionEnvio);
        }

        // GET: DireccionesEnvio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DireccionesEnvio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClienteId,DireccionFisica,EsPredeterminada")] DireccionEnvio direccionEnvio)
        {
            if (!ModelState.IsValid)
            {
                return View(direccionEnvio);
            }

            if (direccionEnvio.EsPredeterminada)
            {
                var otrasDirecciones = await _context.DireccionesEnvio
                    .Where(d => d.ClienteId == direccionEnvio.ClienteId && d.Id != direccionEnvio.Id)
                    .ToListAsync();

                foreach (var direccion in otrasDirecciones)
                {
                    direccion.EsPredeterminada = false;
                }
            }

            _context.Add(direccionEnvio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: DireccionesEnvio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _context.DireccionesEnvio.FindAsync(id);
            if (direccionEnvio == null)
            {
                return NotFound();
            }
            return View(direccionEnvio);
        }

        // POST: DireccionesEnvio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClienteId,DireccionFisica,EsPredeterminada")] DireccionEnvio direccionEnvio)
        {
            if (id != direccionEnvio.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(direccionEnvio);
            }

            if (direccionEnvio.EsPredeterminada)
            {
                var otrasDirecciones = await _context.DireccionesEnvio
                    .Where(d => d.ClienteId == direccionEnvio.ClienteId && d.Id != direccionEnvio.Id)
                    .ToListAsync();

                foreach (var direccion in otrasDirecciones)
                {
                    direccion.EsPredeterminada = false;
                }
            }

            try
            {
                _context.Update(direccionEnvio);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccionEnvioExists(direccionEnvio.Id))
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

        // GET: DireccionesEnvio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccionEnvio = await _context.DireccionesEnvio
                .FirstOrDefaultAsync(m => m.Id == id);
            if (direccionEnvio == null)
            {
                return NotFound();
            }

            return View(direccionEnvio);
        }

        // POST: DireccionesEnvio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccionEnvio = await _context.DireccionesEnvio.FindAsync(id);
            if (direccionEnvio != null)
            {
                _context.DireccionesEnvio.Remove(direccionEnvio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionEnvioExists(int id)
        {
            return _context.DireccionesEnvio.Any(e => e.Id == id);
        }
    }
}
