using System;
using System.Collections.Generic;

namespace Proyecto2API.Models
{
    public partial class LibroStock
    {
        public long LibroStockId { get; set; }
        public long LibroId { get; set; }
        public string Descripcion { get; set; } = null!;
        public long Precio { get; set; }
        public long? ClienteId { get; set; }
        public DateTime FechaIngreso { get; set; }
        public bool LibroRetirado { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual Libro Libro { get; set; } = null!;
    }
}
