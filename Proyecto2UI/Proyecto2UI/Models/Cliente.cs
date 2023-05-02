using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Proyecto2UI.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            LibroStocks = new HashSet<LibroStock>();
            LibrosRetirados = new HashSet<LibroRetirado>();
        }

        public long ClienteId { get; set; }
        public string Nombre { get; set; } = null!;
        [Display(Name = "Cedula")]
        public long ClienteCedula { get; set; }
        [Display(Name = "Fecha de Nacimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public virtual ICollection<LibroStock> LibroStocks { get; set; }

        public virtual ICollection<LibroRetirado> LibrosRetirados { get; set; }
    }
}
