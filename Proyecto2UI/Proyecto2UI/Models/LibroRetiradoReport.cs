using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Proyecto2UI.Models
{
    public partial class LibroRetiradoReport
    {
        public long LibrosRetiradosID { get; set; }
        public long LibroID { get; set; }
        public string NombreLibro { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public long? ClienteID { get; set; }
        public long? LibroStock { get; set; }
        [Display(Name = "Fecha de Retiro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? FechaRetiro { get; set; }

        public virtual Cliente? Cliente { get; set; }
        public virtual Libro Libro { get; set; } = null!;
    }
}
