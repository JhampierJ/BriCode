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
using Microsoft.AspNetCore.Identity;
using Bricons.Areas.Identity.Data;
using Newtonsoft.Json;
using static Bricons.Controllers.CotizacionsController;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bricons.Controllers
{
    public class CotizacionsController : Controller
    {
        private readonly BriconsContext _context;
        
        public struct Coti
        {
            public string Sucursal;
            public string Pais;
            public string Direccion;
            public string Productos;
        }
        public struct ProductosLocal
        {
            public int Id;
            public bool Estado;
            public int Cantidad;
        }
        public CotizacionsController(BriconsContext context)
        {
            _context = context;
     
        }
        public void ActualizarLocalStorage(string datos)
        {
            
            //localSotorague = JsonConvert.DeserializeObject<List<LocalStorage>>(datos);
           // Console.WriteLine("------------------> " + localSotorague.Count);
;
        }
        // GET: Cotizacions
        [HttpGet]
        public async Task<IActionResult> Index(string numeros)
        {
        
            if (numeros == null)
            {
                List<Producto> list = new List<Producto>();
                return View(list);
            }
            int[] numerosArray = numeros.Split(',').Select(int.Parse).ToArray();

            var prodcutos = await _context.Producto.Where(p => numerosArray.Contains(p.Id)).ToArrayAsync();
            
            return View(prodcutos);

        }

        // GET: Cotizacions/Details/5
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

        // GET: Cotizacions/Create
        public IActionResult CreateL()
        {
            string UserActualID =  User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser userActual = _context.ApplicationUsers.FirstOrDefault(u => u.Id == UserActualID);

            Usuario user = _context.Usuario.FirstOrDefault(u => u.Id == userActual.UsuarioId);

            ViewBag.Usuario = user;
            ViewBag.EmailUser = userActual.Email;
            return View();
        }
        [HttpGet]
        public IActionResult MostrarLista()
        {
            string UserActualID = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ApplicationUser userActual = _context.ApplicationUsers.FirstOrDefault(u => u.Id == UserActualID);

            Usuario user = _context.Usuario.FirstOrDefault(u => u.Id == userActual.UsuarioId);

            List<Cotizacion> listCotizacion = new List<Cotizacion>();
           
            listCotizacion = _context.Cotizacion.Where(p => p.UsuarioId == user.Id).ToList();



            return View(listCotizacion);
        }
        public IActionResult MostrarListaAdmin()
        {
            List<Cotizacion> listCotizacion = new List<Cotizacion>();
            listCotizacion = _context.Cotizacion.ToList();


          
            foreach (Cotizacion cot in listCotizacion)
            {
                Usuario user = _context.Usuario.FirstOrDefault(p => p.Id == cot.UsuarioId);
                cot.Usuario = user;
            }

            return View(listCotizacion);
        }
        public IActionResult MostrarProductos()
        {



            return View();
        }
        public ActionResult Crear(string datos)
        {

            Coti coti = JsonConvert.DeserializeObject<Coti>(datos);


            string UserActualID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser userActual = _context.ApplicationUsers.FirstOrDefault(u => u.Id == UserActualID);
            Usuario user = _context.Usuario.FirstOrDefault(u => u.Id == userActual.UsuarioId);

            Cotizacion cotizacion = new Cotizacion();
            cotizacion.Sucursal = coti.Sucursal;
            cotizacion.Pais = coti.Pais;
            cotizacion.Direccion = coti.Direccion;
            cotizacion.UsuarioId = user.Id;
            cotizacion.FechaVencimineto = DateTime.Now.AddDays(7);
            cotizacion.Ciudad = "none";
            cotizacion.Estado = "Revision";
            cotizacion.confirmacion = false;

            _context.Cotizacion.Add(cotizacion);
            _context.SaveChanges();

            List<ProductosLocal> productosLocal = JsonConvert.DeserializeObject <List<ProductosLocal>>(coti.Productos);

            foreach (ProductosLocal prt in productosLocal)
            {
              
                CotizacionProducto cotiProducto = new CotizacionProducto();
                cotiProducto.CotizacionId = cotizacion.Id;
                cotiProducto.ProductoId = prt.Id;
                cotiProducto.Cantidad = prt.Cantidad;

                _context.CotizacionProducto.Add(cotiProducto);
                _context.SaveChanges();
            }



            Console.WriteLine("holaaaaaaa: " + "dfd");
            return new JsonResult(user.Id);
        }
   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Createt(string datos)
        {
  
            return View();

        }
        public IActionResult Revisar(int id)
        {

            string UserActualID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser userActual = _context.ApplicationUsers.FirstOrDefault(u => u.Id == UserActualID);
            Usuario user = _context.Usuario.FirstOrDefault(u => u.Id == userActual.UsuarioId);

            List<CotizacionProducto> cotProducto = _context.CotizacionProducto.Where(p => p.CotizacionId == id).ToList();
            foreach (CotizacionProducto cop in cotProducto)
            {
                cop.Producto = _context.Producto.FirstOrDefault(p=>p.Id == cop.ProductoId);
                        
            }
            ViewBag.Usuario = user;

            return View(cotProducto);
        }
        // GET: Cotizacions/Edit/5
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

        // POST: Cotizacions/Edit/5
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

        // GET: Cotizacions/Delete/5
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

        // POST: Cotizacions/Delete/5
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
