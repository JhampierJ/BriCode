using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricons.Data;
using Bricons.Models;

namespace Bricons.Controllers
{
    public class CotizacionProductoesController : Controller
    {
        private readonly BriconsContext _context;

        public CotizacionProductoesController(BriconsContext context)
        {
            _context = context;
        }

        // GET: CotizacionProductoes
        public async Task<IActionResult> Index()
        {
            var briconsContext = _context.CotizacionProducto.Include(c => c.Cotizacion).Include(c => c.Producto);
            return View(await briconsContext.ToListAsync());
        }

        // GET: CotizacionProductoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CotizacionProducto == null)
            {
                return NotFound();
            }

            var cotizacionProducto = await _context.CotizacionProducto
                .Include(c => c.Cotizacion)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.CotizacionId == id);
            if (cotizacionProducto == null)
            {
                return NotFound();
            }

            return View(cotizacionProducto);
        }

        // GET: CotizacionProductoes/Create
        public IActionResult Create()
        {
            ViewData["CotizacionId"] = new SelectList(_context.Cotizacion, "Id", "Ciudad");
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion");
            return View();
        }

        // POST: CotizacionProductoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CotizacionProducto cotizacionProducto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotizacionProducto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CotizacionId"] = new SelectList(_context.Cotizacion, "Id", "Ciudad", cotizacionProducto.CotizacionId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", cotizacionProducto.ProductoId);
            return View(cotizacionProducto);
        }

        // GET: CotizacionProductoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CotizacionProducto == null)
            {
                return NotFound();
            }

            var cotizacionProducto = await _context.CotizacionProducto.FindAsync(id);
            if (cotizacionProducto == null)
            {
                return NotFound();
            }
            ViewData["CotizacionId"] = new SelectList(_context.Cotizacion, "Id", "Ciudad", cotizacionProducto.CotizacionId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", cotizacionProducto.ProductoId);
            return View(cotizacionProducto);
        }

        // POST: CotizacionProductoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CotizacionId,ProductoId,Cantidad")] CotizacionProducto cotizacionProducto)
        {
            if (id != cotizacionProducto.CotizacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotizacionProducto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotizacionProductoExists(cotizacionProducto.CotizacionId))
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
            ViewData["CotizacionId"] = new SelectList(_context.Cotizacion, "Id", "Ciudad", cotizacionProducto.CotizacionId);
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "Descripcion", cotizacionProducto.ProductoId);
            return View(cotizacionProducto);
        }

        // GET: CotizacionProductoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CotizacionProducto == null)
            {
                return NotFound();
            }

            var cotizacionProducto = await _context.CotizacionProducto
                .Include(c => c.Cotizacion)
                .Include(c => c.Producto)
                .FirstOrDefaultAsync(m => m.CotizacionId == id);
            if (cotizacionProducto == null)
            {
                return NotFound();
            }

            return View(cotizacionProducto);
        }

        // POST: CotizacionProductoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CotizacionProducto == null)
            {
                return Problem("Entity set 'BriconsContext.CotizacionProducto'  is null.");
            }
            var cotizacionProducto = await _context.CotizacionProducto.FindAsync(id);
            if (cotizacionProducto != null)
            {
                _context.CotizacionProducto.Remove(cotizacionProducto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotizacionProductoExists(int id)
        {
          return (_context.CotizacionProducto?.Any(e => e.CotizacionId == id)).GetValueOrDefault();
        }
    }
}
