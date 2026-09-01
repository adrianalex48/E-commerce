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
    public class IncidenciasDevolucionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IncidenciasDevolucionController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: IncidenciasDevolucion
        public async Task<IActionResult> Index()
        {
            return View(await _context.IncidenciasDevolucion.ToListAsync());
        }

        // GET: IncidenciasDevolucion/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidenciaDevolucion = await _context.IncidenciasDevolucion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidenciaDevolucion == null)
            {
                return NotFound();
            }

            return View(incidenciaDevolucion);
        }

        // GET: IncidenciasDevolucion/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IncidenciasDevolucion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BodegaId,ProductoId,Fecha,Motivo,Costo,CreditoGenerado")] IncidenciaDevolucion incidenciaDevolucion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(incidenciaDevolucion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(incidenciaDevolucion);
        }

        // GET: IncidenciasDevolucion/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidenciaDevolucion = await _context.IncidenciasDevolucion.FindAsync(id);
            if (incidenciaDevolucion == null)
            {
                return NotFound();
            }
            return View(incidenciaDevolucion);
        }

        // POST: IncidenciasDevolucion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BodegaId,ProductoId,Fecha,Motivo,Costo,CreditoGenerado")] IncidenciaDevolucion incidenciaDevolucion)
        {
            if (id != incidenciaDevolucion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(incidenciaDevolucion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidenciaDevolucionExists(incidenciaDevolucion.Id))
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
            return View(incidenciaDevolucion);
        }

        // GET: IncidenciasDevolucion/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidenciaDevolucion = await _context.IncidenciasDevolucion
                .FirstOrDefaultAsync(m => m.Id == id);
            if (incidenciaDevolucion == null)
            {
                return NotFound();
            }

            return View(incidenciaDevolucion);
        }

        // POST: IncidenciasDevolucion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var incidenciaDevolucion = await _context.IncidenciasDevolucion.FindAsync(id);
            if (incidenciaDevolucion != null)
            {
                _context.IncidenciasDevolucion.Remove(incidenciaDevolucion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IncidenciaDevolucionExists(int id)
        {
            return _context.IncidenciasDevolucion.Any(e => e.Id == id);
        }
    }
}
