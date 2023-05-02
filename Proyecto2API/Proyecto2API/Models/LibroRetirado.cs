using System;
using System.Collections.Generic;

namespace Proyecto2API.Models
{
    public partial class LibroRetirado
    {
        public long LibroRetiradoId { get; set; }
        public long LibroId { get; set; }
        public string NombreLibro { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public long? ClienteId { get; set; }
        public long? LibroStock { get; set; }
        public DateTime? FechaRetiro { get; set; }


        public virtual Cliente? Cliente { get; set; }
        public virtual Libro Libro { get; set; } = null!;
    }
}
