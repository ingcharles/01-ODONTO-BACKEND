using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models.User
{
    public class Menu : Auditoria
    {
        
        public long? cod_menu { get; set; } = default!;
        public string nom_menu { get; set; } = default!;

        public string des_menu { get; set; } = default!;
        public string ico_menu { get; set; } = default!;
        public string lin_menu { get; set; } = default!;
        public string nmo_menu { get; set; } = default!;
        public long? cod_menu_padre { get; set; } = default!;
        public long? cod_aplicacion { get; set; } = default!;
        public long? cod_usuario { get; set; } = default!;
        public Int32 est_menu { get; set; } = default!;
        public string mensaje_logica { get; set; } = default!;
    }
}
