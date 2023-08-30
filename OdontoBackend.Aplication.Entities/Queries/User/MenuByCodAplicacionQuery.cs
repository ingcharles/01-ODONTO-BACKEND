using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Queries.User
{
    public class UserByCodQuery
    {
        public string auditoria { get; set; } = default!;

        public long? codigoUsuario { get; set; } = default!;


    }
}
