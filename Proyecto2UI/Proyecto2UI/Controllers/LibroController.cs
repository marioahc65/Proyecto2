using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2UI.Models;
using Proyecto2UI.Servicios;

namespace Proyecto2UI.Controllers
{
    public class LibroController : Controller
    {
        private readonly IServicio_API _servicio;

        public LibroController(IServicio_API servicio)
        {
            _servicio = servicio;
        }
        // GET: LibroController
        public async Task<ActionResult> Index()
        {
            List<Libro> libros = new List<Libro>();
            try
            {

            libros = await _servicio.ObtenerLibros();

            return View(libros);
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LibroController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibroController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Libro libro = new Libro();
                libro.Nombre = collection["Nombre"];
                libro.Empresa = collection["Empresa"];

                libro = await _servicio.CrearLibro(libro);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LibroController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LibroController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
