using OdontoBackend.Aplicacion.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.ViewModels.User
{
    public class MenuViewModel
    {
        public long? codigo { get; set; } = default!;
        public string? nombre { get; set; } = default!;
        public string? descripcion { get; set; } = default!;
        public string? icono { get; set; } = default!;
        public string? link { get; set; } = default!;
        public long? codigoPadre { get; set; } = default!;
        public long? codigoAplicacion { get; set; } = default!;
        public Int32? estado { get; set; } = default!;
        public string? mensajeLogica { get; set; } = default!;
    }
}
