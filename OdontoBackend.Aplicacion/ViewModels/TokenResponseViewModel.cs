using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.ViewModels
{
    public class TokenResponseViewModel
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
    }
}
