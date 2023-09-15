using OdontoBackend.Aplicacion.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.ViewModels.User
{
    public class AplicacionViewModel
    {
        public long? codigo { get; set; } = default!;
        public string? nombre { get; set; } = default!;
        public string? descripcion { get; set; } = default!;
        public string? icono { get; set; } = default!;
        public string? color { get; set; } = default!;
        public string? nemonico { get; set; } = default!;
        public Int32? estado { get; set; } = default!;
        public string? mensajeLogica { get; set; } = default!;
        public long? codigoUsuario { get; set; } = default!;
    }
}
