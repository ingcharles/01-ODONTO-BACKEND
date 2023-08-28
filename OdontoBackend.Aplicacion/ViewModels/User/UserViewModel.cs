using OdontoBackend.Aplicacion.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.ViewModels.User
{
    public class UserViewModel
    {
        [DisplayName("Código usuario")]
        public long? codigoUsuario { get; set; } = default!;
        [DisplayName("Código usuario")]
        public string? nombreUsuario { get; set; } = default!;
        public string? mensajeLogica { get; set; } = default!;

        public List<RefreshToken> refreshTokens { get; set; } = default!;
    }
}
