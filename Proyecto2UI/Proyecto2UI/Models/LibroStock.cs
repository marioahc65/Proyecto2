using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2UI.Models
{
    public partial class LibroStock
    {
        public long LibroStockId { get; set; }
        public long LibroId { get; set; }
        public string Descripcion { get; set; } = null!;
        public long Precio { get; set; }
        public long? ClienteId { get; set; }
        [Display(Name = "Fecha de Ingreso")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        public bool LibroRetirado { get; set; }
        public virtual Cliente? Cliente { get; set; }
        public virtual Libro Libro { get; set; } = null!;
    }
}
