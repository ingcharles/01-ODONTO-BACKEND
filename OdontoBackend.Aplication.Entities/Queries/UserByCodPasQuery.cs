using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Queries
{
    public class UserByCodPasQuery
    {
        [DisplayName("Dni Usuario")]
        public string dni { get; set; } = default!;
        [DisplayName("Nombre Usuario")]
        public string password { get; set; } = default!;
    }
}
