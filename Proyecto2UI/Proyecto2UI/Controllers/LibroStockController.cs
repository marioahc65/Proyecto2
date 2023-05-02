using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2UI.Models;
using Proyecto2UI.Servicios;
using System.Collections.Generic;

namespace Proyecto2UI.Controllers
{
    public class LibroStockController : Controller
    {
        private readonly IServicio_API _servicio;

        public LibroStockController(IServicio_API servicio)
        {
            _servicio = servicio;
        }
        // GET: LibroRetirado
        public ActionResult Index()
        {
            return View();
        }

        // GET: LibroRetirado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibroRetirado/Create
        public async Task<ActionResult> Create()
        {
            List<Libro> libros = new List<Libro>();
            try
            {
                libros = await _servicio.ObtenerLibros();
                ViewBag.Libros = libros;
            }
            catch(Exception)
            {
                throw;
            }

            return View();
        }

        // POST: LibroRetirado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                LibroStock libroStock = new LibroStock();
                libroStock.LibroId = Convert.ToInt64(collection["LibroId"]);
                libroStock.Descripcion = collection["Descripcion"];
                libroStock.Precio = Convert.ToInt64(collection["Precio"]);
                libroStock.FechaIngreso = Convert.ToDateTime(collection["FechaIngreso"]);

                libroStock = await _servicio.CrearLibroStock(libroStock);

                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroRetirado/Edit/5
        public async Task<ActionResult> Edit()
        {
            List<LibroStock> libroStock = new List<LibroStock>();

            libroStock = await _servicio.ObtenerLibroStockSinReservar();
            return View(libroStock);
        }

        // POST: LibroRetirado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var clienteId = Convert.ToInt64(collection["ClienteId"]);
                LibroStock libroStock = new LibroStock();
                libroStock = await _servicio.ObtenerLibroStockId(id);
                Cliente cliente = new Cliente();
                cliente = await _servicio.ObtenerCliente(clienteId);

                if (cliente.ClienteId == 0)
                {
                    TempData["Message"] = "El cliente " + clienteId + " no existe";

                    return RedirectToAction(nameof(Edit));
                }
                TempData["Message"] = "El libro se reservo con exito";

                libroStock.ClienteId = clienteId;

                libroStock = await _servicio.ModificarLibroStock(id,libroStock);

                return RedirectToAction(nameof(Edit));
            }
            catch
            {
                return View();
            }
        }
    }
}
