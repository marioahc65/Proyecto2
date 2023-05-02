using AspNetCore.Reporting;
using Microsoft.AspNetCore.Mvc;
using Proyecto2UI.Models;
using Proyecto2UI.Servicios;

namespace Proyecto2UI.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IServicio_API _servicio;

        public ReportController(IWebHostEnvironment webHostEnvironment, IServicio_API servicio)
        {
            _servicio = servicio;
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> PrintStock()
        {
           string mimeType = "";
              int extension = 1;
              var path = $"{this._webHostEnvironment.WebRootPath}\\Reportes\\stock.rdlc";
                Dictionary<string, string> parameters = new Dictionary<string, string>();
                //parameters.Add("rp1", "Welcome to FoxLearn");
                LocalReport localReport = new LocalReport(path);

            List<Models.LibroStock> data = await _servicio.ObtenerLibroStock();
            List<LibroStockReport> libroStockReports = new List<LibroStockReport>();

            foreach (var item in data.Where(x=>x.LibroRetirado == false))
            {
                LibroStockReport libroStockReport = new LibroStockReport();
                libroStockReport.LibroStockID = item.LibroId;
                libroStockReport.Descripcion = item.Descripcion;
                libroStockReport.Precio = item.Precio;
                libroStockReport.FechaIngreso = item.FechaIngreso;
                libroStockReport.ClienteID = item.ClienteId;
                libroStockReports.Add(libroStockReport);
            }

            localReport.AddDataSource("DataSet1", libroStockReports);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
                return File(result.MainStream, "application/pdf");
        }

        public async Task<ActionResult> Retirados(DateTime FechaInicio, DateTime FechaFin)
        {
            string mimeType = "";
            int extension = 1;
            var path = $"{this._webHostEnvironment.WebRootPath}\\Reportes\\retirados.rdlc";
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("FechaInicio", FechaInicio.ToString());
            parameters.Add("FechaFin",FechaFin.ToString());

            LocalReport localReport = new LocalReport(path);

            List<LibroRetirado> data = await _servicio.ObtenerLibroRetiradoPorFecha(FechaInicio,FechaFin);
            List<LibroRetiradoReport> RetiroReports = new List<LibroRetiradoReport>();

            foreach (var item in data)
            {
                LibroRetiradoReport RetiroReport = new LibroRetiradoReport();
                RetiroReport.LibrosRetiradosID = item.LibroRetiradoId;
                RetiroReport.Descripcion = item.Descripcion;
                RetiroReport.LibroID = item.LibroId;
                RetiroReport.NombreLibro = item.NombreLibro;
                RetiroReport.ClienteID = item.ClienteId;
                RetiroReport.FechaRetiro = item.FechaRetiro;
                RetiroReports.Add(RetiroReport);
            }

            localReport.AddDataSource("DataSet1", RetiroReports);

            var result = localReport.Execute(RenderType.Pdf, extension, parameters, mimeType);
            return File(result.MainStream, "application/pdf");
        }
    }
}
