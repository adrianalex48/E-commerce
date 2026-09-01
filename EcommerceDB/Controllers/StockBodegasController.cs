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
    public class StockBodegasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockBodegasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StockBodegas
        public async Task<IActionResult> Index()
        {
            return View(await _context.StockBodegas.ToListAsync());
        }

        // GET: StockBodegas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockBodega = await _context.StockBodegas
                .FirstOrDefaultAsync(m => m.BodegaId == id);
            if (stockBodega == null)
            {
                return NotFound();
            }

            return View(stockBodega);
        }

        // GET: StockBodegas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockBodegas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BodegaId,ProductoId,Cantidad")] StockBodega stockBodega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockBodega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockBodega);
        }

        // GET: StockBodegas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockBodega = await _context.StockBodegas.FindAsync(id);
            if (stockBodega == null)
            {
                return NotFound();
            }
            return View(stockBodega);
        }

        // POST: StockBodegas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BodegaId,ProductoId,Cantidad")] StockBodega stockBodega)
        {
            if (id != stockBodega.BodegaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockBodega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockBodegaExists(stockBodega.BodegaId))
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
            return View(stockBodega);
        }

        // GET: StockBodegas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stockBodega = await _context.StockBodegas
                .FirstOrDefaultAsync(m => m.BodegaId == id);
            if (stockBodega == null)
            {
                return NotFound();
            }

            return View(stockBodega);
        }

        // POST: StockBodegas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stockBodega = await _context.StockBodegas.FindAsync(id);
            if (stockBodega != null)
            {
                _context.StockBodegas.Remove(stockBodega);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockBodegaExists(int id)
        {
            return _context.StockBodegas.Any(e => e.BodegaId == id);
        }
    }
}
