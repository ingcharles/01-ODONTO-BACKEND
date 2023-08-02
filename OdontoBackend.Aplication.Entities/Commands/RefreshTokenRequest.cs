using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Commands
{
    public class RefreshTokenRequest
    {
        public Int64? codigoUsuario { get; set; }
        public string refreshToken { get; set; }

    }
}
