using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricons.Data;
using Bricons.Models;
using Bricons.Areas.Identity.Data;
using System.Security.Claims;
using Bricons.Utilities;

namespace Bricons.Controllers
{
    public class ProgramacionesController : Controller
    {
        private readonly BriconsContext _context;

        public ProgramacionesController(BriconsContext context)
        {
            _context = context;
        }

        // GET: Programaciones
        public async Task<IActionResult> Index()
        {
            var briconsContext = _context.Programacion.Include(p => p.Producto).Include(p => p.User);
            return View(await briconsContext.ToListAsync());
        }
        public async Task<IActionResult> IndexU()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser us = _context.ApplicationUsers.Find(userId);
            Usuario usuario = _context.Usuario.Find(us.UsuarioId);
            var briconsContext = _context.Programacion.Include(p => p.Producto).Where(u => u.UserId == usuario.Id);
            return View(await briconsContext.ToListAsync());
        }

        // GET: Programaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Programacion == null)
            {
                return NotFound();
            }

            var programacion = await _context.Programacion
                .Include(p => p.Producto)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programacion == null)
            {
                return NotFound();
            }

            return View(programacion);
        }

        // GET: Programaciones/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto");
            ViewData["UserId"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }
        public IActionResult CreateU()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser us = _context.ApplicationUsers.Find(userId);
            Usuario usuario = _context.Usuario.Find(us.UsuarioId);
            ViewData["Usuario"] = usuario;
            return View();
        }
        // POST: Programaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Fecha,ProductoId,UserId")] Programacion programacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programacion);
                await _context.SaveChangesAsync();

                if (User.IsInRole(CNT.Admin))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(IndexU));
                }

               
            }
            else
            {
                Console.WriteLine("holaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", programacion.ProductoId);
            ViewData["UserId"] = new SelectList(_context.Usuario, "Id", "Id", programacion.UserId);
            return View(programacion);
        }

        // GET: Programaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Programacion == null)
            {
                return NotFound();
            }

            var programacion = await _context.Programacion.FindAsync(id);
            if (programacion == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", programacion.ProductoId);
            ViewData["UserId"] = new SelectList(_context.Usuario, "Id", "Id", programacion.UserId);
            return View(programacion);
        }
        public async Task<IActionResult> EditU(int? id)
        {
            if (id == null || _context.Programacion == null)
            {
                return NotFound();
            }

            var programacion = await _context.Programacion.FindAsync(id);
            if (programacion == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", programacion.ProductoId);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser us = _context.ApplicationUsers.Find(userId);
            Usuario usuario = _context.Usuario.Find(us.UsuarioId);
            ViewData["Usuario"] = usuario;
            return View(programacion);
        }
        // POST: Programaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Fecha,ProductoId,UserId")] Programacion programacion)
        {
            if (id != programacion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramacionExists(programacion.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (User.IsInRole(CNT.Admin))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(IndexU));
                }

             
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", programacion.ProductoId);
            ViewData["UserId"] = new SelectList(_context.Usuario, "Id", "Id", programacion.UserId);
            return View(programacion);
        }

        // GET: Programaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Programacion == null)
            {
                return NotFound();
            }

            var programacion = await _context.Programacion
                .Include(p => p.Producto)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (programacion == null)
            {
                return NotFound();
            }

            return View(programacion);
        }

        // POST: Programaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Programacion == null)
            {
                return Problem("Entity set 'BriconsContext.Programacion'  is null.");
            }
            var programacion = await _context.Programacion.FindAsync(id);
            if (programacion != null)
            {
                _context.Programacion.Remove(programacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramacionExists(int id)
        {
          return (_context.Programacion?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
