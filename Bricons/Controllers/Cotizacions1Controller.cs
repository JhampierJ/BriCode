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
    public class Cotizacions1Controller : Controller
    {
        private readonly BriconsContext _context;

        public Cotizacions1Controller(BriconsContext context)
        {
            _context = context;
        }

        // GET: Cotizacions1
        public async Task<IActionResult> Index()
        {
            var briconsContext = _context.Cotizacion.Include(c => c.Usuario);
            return View(await briconsContext.ToListAsync());
        }

        // GET: Cotizacions1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cotizacion == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizacion
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            return View(cotizacion);
        }

        // GET: Cotizacions1/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }

        // POST: Cotizacions1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,FechaVencimineto,Sucursal,Pais,Ciudad,Direccion,Estado,confirmacion")] Cotizacion cotizacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cotizacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", cotizacion.UsuarioId);
            return View(cotizacion);
        }

        // GET: Cotizacions1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cotizacion == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizacion.FindAsync(id);
            if (cotizacion == null)
            {
                return NotFound();
            }
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", cotizacion.UsuarioId);
            return View(cotizacion);
        }

        // POST: Cotizacions1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,FechaVencimineto,Sucursal,Pais,Ciudad,Direccion,Estado,confirmacion")] Cotizacion cotizacion)
        {
            if (id != cotizacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cotizacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CotizacionExists(cotizacion.Id))
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
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", cotizacion.UsuarioId);
            return View(cotizacion);
        }

        // GET: Cotizacions1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cotizacion == null)
            {
                return NotFound();
            }

            var cotizacion = await _context.Cotizacion
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cotizacion == null)
            {
                return NotFound();
            }

            return View(cotizacion);
        }

        // POST: Cotizacions1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cotizacion == null)
            {
                return Problem("Entity set 'BriconsContext.Cotizacion'  is null.");
            }
            var cotizacion = await _context.Cotizacion.FindAsync(id);
            if (cotizacion != null)
            {
                _context.Cotizacion.Remove(cotizacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CotizacionExists(int id)
        {
          return (_context.Cotizacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
