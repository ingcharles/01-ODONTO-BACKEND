using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models
{
    public class UserCommandFrom : Auditoria
    {
        public Int64? cod_usuario { get; set; } = default!;
        public string mensaje_logica { get; set; } = default!;

    }
}
