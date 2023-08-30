using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Queries.User
{
    public class MenuByCodAplicacionQuery
    {
        public string auditoria { get; set; } = default!;

        public long? codigoUsuario { get; set; } = default!;
        public long? codigoAplicacion { get; set; } = default!;

    }
}
