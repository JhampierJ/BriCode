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
    public class CategoriumsController : Controller
    {
        private readonly BriconsContext _context;

        public CategoriumsController(BriconsContext context)
        {
            _context = context;
        }

        // GET: Categoriums
        public async Task<IActionResult> Index()
        {
              return _context.Categorium != null ? 
                          View(await _context.Categorium.ToListAsync()) :
                          Problem("Entity set 'BriconsContext.Categorium'  is null.");
        }

        // GET: Categoriums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categorium == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categorium
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorium == null)
            {
                return NotFound();
            }

            return View(categorium);
        }

        // GET: Categoriums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoriums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCategoria")] Categorium categorium)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categorium);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categorium);
        }

        // GET: Categoriums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categorium == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categorium.FindAsync(id);
            if (categorium == null)
            {
                return NotFound();
            }
            return View(categorium);
        }

        // POST: Categoriums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCategoria")] Categorium categorium)
        {
            if (id != categorium.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categorium);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriumExists(categorium.Id))
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
            return View(categorium);
        }

        // GET: Categoriums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categorium == null)
            {
                return NotFound();
            }

            var categorium = await _context.Categorium
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categorium == null)
            {
                return NotFound();
            }

            return View(categorium);
        }

        // POST: Categoriums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categorium == null)
            {
                return Problem("Entity set 'BriconsContext.Categorium'  is null.");
            }
            var categorium = await _context.Categorium.FindAsync(id);
            if (categorium != null)
            {
                _context.Categorium.Remove(categorium);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriumExists(int id)
        {
          return (_context.Categorium?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}