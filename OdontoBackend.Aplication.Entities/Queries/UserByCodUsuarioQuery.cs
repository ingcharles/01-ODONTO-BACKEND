using OdontoBackend.Aplication.Entities.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Queries
{
    public class UserByCodUsuarioQuery : AuditoriaCommand
    {
        public Int64? codigoUsuario { get; set; } = default!;
    }
}
