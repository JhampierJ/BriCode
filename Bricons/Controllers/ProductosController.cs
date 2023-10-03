﻿using System;
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
    public class ProductosController : Controller
    {
        private readonly BriconsContext _context;

        public ProductosController(BriconsContext context)
        {
            _context = context;
        }

        // GET: Productos
        public async Task<IActionResult> Index(int cat)
        {
    
            var prodcutos = await _context.Producto.Where(p=>p.CategoriaID==cat).ToArrayAsync();;


            return View(prodcutos);
        }
        public async Task<IActionResult> Categorias()
        {
            return View();
        }
        public ActionResult BuscarProductos(string palb)
        {
            var productosEncontrados = new List<Producto>();
            if (palb == null) return Json(productosEncontrados);

            var productos = _context.Producto.ToList();
          

            foreach (var producto in productos)
            {
                string nombreP = producto.NombreProducto.ToUpper();
                bool conicide = true;
                for (int i = 0; i < palb.Length && i < nombreP.Length; i++)
                {

                    if (nombreP[i] != palb[i])
                    {
                        conicide = false;
                        break;
                    } 
                }
                if(conicide) productosEncontrados.Add(producto);
            }

            return Json(productosEncontrados);
        }


        // GET: Productos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Productos/Create
        public IActionResult Create()
        {
            ViewData["CategoriaID"] = new SelectList(_context.Categorium, "Id", "NombreCategoria");
            return View();
        }

        // POST: Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaID,NombreProducto,Stock,Imagen,Descripcion,Peso,Largo,Ancho,Altura")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaID"] = new SelectList(_context.Categorium, "Id", "NombreCategoria", producto.CategoriaID);
            return View(producto);
        }

        // GET: Productos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaID"] = new SelectList(_context.Categorium, "Id", "NombreCategoria", producto.CategoriaID);
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaID,NombreProducto,Stock,Imagen,Descripcion,Peso,Largo,Ancho,Altura")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["CategoriaID"] = new SelectList(_context.Categorium, "Id", "NombreCategoria", producto.CategoriaID);
            return View(producto);
        }

        // GET: Productos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Producto == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Producto == null)
            {
                return Problem("Entity set 'BriconsContext.Producto'  is null.");
            }
            var producto = await _context.Producto.FindAsync(id);
            if (producto != null)
            {
                _context.Producto.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Producto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
