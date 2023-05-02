using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto2API.Models
{
    public partial class Libro
    {
        public Libro()
        {
            LibroStocks = new HashSet<LibroStock>();
            LibrosRetirados = new HashSet<LibroRetirado>();
        }

        public long LibroId { get; set; }
        public string Nombre { get; set; } = null!;
        public string Empresa { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<LibroStock> LibroStocks { get; set; }
        [JsonIgnore]
        public virtual ICollection<LibroRetirado> LibrosRetirados { get; set; }
    }
}
