using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Queries
{
    public class UserByCiPasQuery
    {
        [DisplayName("Cédula Usuario")]
        public string ci { get; set; } = default!;
        [DisplayName("Nombre Usuario")]
        public string password { get; set; } = default!;
    }
}
