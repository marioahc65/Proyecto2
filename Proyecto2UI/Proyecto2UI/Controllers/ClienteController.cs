using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto2UI.Models;
using Proyecto2UI.Servicios;

namespace Proyecto2UI.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IServicio_API _servicio;

        public ClienteController(IServicio_API servicio)
        {
            _servicio = servicio;
        }
        // GET: ClienteController
        public async Task<ActionResult> Index()
        {
            List<Cliente> clientes = new List<Cliente>();
            try
            {
                clientes = await _servicio.ObtenerClientes();
                return View(clientes);
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> ListaStock()
        {
            try
            {
                List<Cliente> clientes = new List<Cliente>();

                clientes = await _servicio.ObtenerClientes();

                foreach (var item in clientes)
                {
                    item.LibroStocks = await _servicio.ObtenerLibroStockCliente(item.ClienteId);
                }

                return View(clientes);
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Nombre = collection["Nombre"];
                cliente.ClienteCedula = Convert.ToInt64(collection["ClienteCedula"]);
                cliente.FechaNacimiento = Convert.ToDateTime(collection["FechaNacimiento"]);

                cliente = await _servicio.CrearCliente(cliente);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteController/Edit/5
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
