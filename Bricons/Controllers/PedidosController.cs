using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricons.Data;
using Bricons.Models;
using System.Security.Claims;
using Bricons.Areas.Identity.Data;
using Bricons.Utilities;

namespace Bricons.Controllers
{
    public class PedidosController : Controller
    {
        private readonly BriconsContext _context;

        public PedidosController(BriconsContext context)
        {
            _context = context;
        }

        // GET: Pedidos
        public async Task<IActionResult> Index()
        {
            var briconsContext = _context.Pedido.Include(p => p.Producto).Include(p => p.Usuario);
            return View(await briconsContext.ToListAsync());
        }
        public async Task<IActionResult> IndexU()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser us = _context.ApplicationUsers.Find(userId);
            Usuario usuario = _context.Usuario.Find(us.UsuarioId);

            var briconsContext = _context.Pedido.Include(p => p.Producto).Include(p => p.Usuario).Where(u => u.UsuarioId==usuario.Id);
            return View(await briconsContext.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Producto)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Pedidos/Create
        public IActionResult Create()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id");
            return View();
        }
        public IActionResult CreateUW()
        {
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto");
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser us = _context.ApplicationUsers.Find(userId);
            Usuario usuario = _context.Usuario.Find(us.UsuarioId);
            ViewData["Usuario"] = usuario;
            return View();
        }

        // POST: Pedidos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,ProductoId,Fecha")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", pedido.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }
        public async Task<IActionResult> CreateU([Bind("Id,UsuarioId,ProductoId,Fecha")] Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexU));
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", pedido.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }
        // GET: Pedidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", pedido.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }
        public async Task<IActionResult> EditUW(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", pedido.ProductoId);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ApplicationUser us = _context.ApplicationUsers.Find(userId);
            Usuario usuario = _context.Usuario.Find(us.UsuarioId);
            ViewData["Usuario"] = usuario;
            return View(pedido);
        }

        // POST: Pedidos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,ProductoId,Fecha")] Pedido pedido)
        {
            if (id != pedido.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PedidoExists(pedido.Id))
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
            ViewData["ProductoId"] = new SelectList(_context.Producto, "Id", "NombreProducto", pedido.ProductoId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Id", pedido.UsuarioId);
            return View(pedido);
        }
        

        // GET: Pedidos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pedido == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedido
                .Include(p => p.Producto)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pedido == null)
            {
                return Problem("Entity set 'BriconsContext.Pedido'  is null.");
            }
            var pedido = await _context.Pedido.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedido.Remove(pedido);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(int id)
        {
          return (_context.Pedido?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
