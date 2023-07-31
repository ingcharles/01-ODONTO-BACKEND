using OdontoBackend.Aplicacion.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.ViewModels
{
    public class UserViewModel
    {
        [DisplayName("Código usuario")]
        public Int64? codigoUsuario { get; set; } = default!;
        [DisplayName("Código usuario")]
        public String? nombreUsuario { get; set; } = default!;
        public String? mensajeLogica { get; set; } = default!;

        public List<RefreshToken> refreshTokens { get; set; } = default!;
    }
}
