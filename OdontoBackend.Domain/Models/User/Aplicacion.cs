using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models.User
{
    public class Aplicacion : Auditoria
    {
        
        public long? cod_aplicacion { get; set; } = default!;
        public string nom_aplicacion { get; set; } = default!;

        public string des_aplicacion { get; set; } = default!;
        public string ico_aplicacion { get; set; } = default!;
        public string col_aplicacion { get; set; } = default!;
        public string nmo_aplicacion { get; set; } = default!;
        public Int32 est_aplicacion { get; set; } = default!;
        public string mensaje_logica { get; set; } = default!;
    }
}
