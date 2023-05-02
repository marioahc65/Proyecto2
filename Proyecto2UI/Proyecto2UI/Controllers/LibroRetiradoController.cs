using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2UI.Models;
using Proyecto2UI.Servicios;

namespace Proyecto2UI.Controllers
{
    public class LibroRetiradoController : Controller
    {
        private readonly IServicio_API _servicio;

        public LibroRetiradoController(IServicio_API servicio)
        {
            _servicio = servicio;
        }
        // GET: LibroRetiradoController
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: LibroRetiradoController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            return View();

        }

        // GET: LibroRetiradoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LibroRetiradoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: LibroRetiradoController/Edit/5
        public async Task<ActionResult> Edit(long CLienteId)
        {
            Cliente cliente = new Cliente();
            cliente = await _servicio.ObtenerCliente(CLienteId);
            
            if(cliente.ClienteId == 0)
            {
                TempData["Message"] = "El cliente " + CLienteId + " no existe";
                return RedirectToAction("Index", "LibroRetirado");
            }

            List<LibroStock> libroStock = new List<LibroStock>();

            libroStock = await _servicio.ObtenerLibroStock();

            libroStock = libroStock.Where(x => x.ClienteId == CLienteId && x.LibroRetirado == false).ToList();

            return View(libroStock);
        }

        // POST: LibroRetiradoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                var clienteId = Convert.ToInt64(collection["ClienteId"]);

                LibroStock libroStock = new LibroStock();
                libroStock = await _servicio.ObtenerLibroStockId(id);

                LibroRetirado libroRetirado = new LibroRetirado();
                
                libroRetirado.NombreLibro = libroStock.Libro.Nombre;
                libroRetirado.Descripcion = libroStock.Descripcion;
                libroRetirado.ClienteId = libroStock.ClienteId;
                libroRetirado.LibroId = libroStock.LibroId;
                libroRetirado.LibroStock = libroStock.LibroStockId;
                libroRetirado.FechaRetiro = DateTime.Now;

                libroRetirado = await _servicio.CrearLibroRetirado(libroRetirado);    

                TempData["Message"] = "El cliente " + clienteId + " retiro el libro "+ libroStock.Libro.Nombre;

                return RedirectToAction("Edit", new { ClienteId = clienteId });
            }
            catch
            {
                return View();
            }
        }
    }
}
