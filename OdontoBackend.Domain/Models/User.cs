using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models
{
    public class User : Auditoria
    {
        public Int64? cod_usuario { get; set; } = default!;
        public string dni_usuario { get; set; } = default!;
        public string pas_usuario { get; set; } = default!;
        public string mensaje_logica { get; set; } = default!;

    }
}
