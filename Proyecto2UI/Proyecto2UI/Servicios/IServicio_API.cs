using Proyecto2UI.Models;

namespace Proyecto2UI.Servicios
{
    public interface IServicio_API
    {
        Task<List<Cliente>> ObtenerClientes();
        Task<Cliente> ObtenerCliente(long ClienteId);
        Task<Cliente> CrearCliente(Cliente objeto);
        Task<Cliente> ModificarCliente(long ClienteId, Cliente objeto);

        Task<List<Libro>> ObtenerLibros();
        Task<Libro> ObtenerLibro(long LibroId);
        Task<Libro> CrearLibro(Libro objeto);
        Task<Libro> ModificarLibro(long LibroId, Libro objeto);

        Task<List<LibroStock>> ObtenerLibroStockCliente(long ClienteId);
        Task<List<LibroStock>> ObtenerLibroStock();
        Task<LibroStock> ObtenerLibroStockId(long LibroStockId);
        Task<List<LibroStock>> ObtenerLibroStockSinReservar();
        Task<LibroStock> CrearLibroStock(LibroStock objeto);
        Task<LibroStock> ModificarLibroStock(long LibroStockId, LibroStock objeto);

        Task<List<LibroRetirado>> ObtenerLibroRetiradoPorFecha(DateTime FechaInicio, DateTime FechaFinal);
        Task<LibroRetirado> CrearLibroRetirado(LibroRetirado objeto);
        Task<LibroRetirado> ModificarLibroRetirado(long LibroRetiradoId, LibroStock objeto);

    }
}
