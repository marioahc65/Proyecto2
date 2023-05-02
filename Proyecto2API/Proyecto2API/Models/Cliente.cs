using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Proyecto2API.Models
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
        public long ClienteCedula { get; set; }
        public DateTime FechaNacimiento { get; set; }

        [JsonIgnore]
        public virtual ICollection<LibroStock> LibroStocks { get; set; }
        [JsonIgnore]
        public virtual ICollection<LibroRetirado> LibrosRetirados { get; set; }
    }
}
