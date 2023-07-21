using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models
{
    public class User : Auditoria
    {
        public Int64 cod_usuario { get; set; } = default!;
        public String dni_usuario { get; set; } = default!;

        public String pas_usuario { get; set; } = default!;
        public String nom_usuario { get; set; } = default!;
        public String ape_usuario { get; set; } = default!;
        public Boolean lic_agr_usuario { get; set; } = default!;
        public String mai_usuario { get; set; } = default!;
        public Boolean? is_pro_usuario { get; set; } = default!;
        public Boolean? is_cli_usuario { get; set; } = default!;
        public string mensaje_logica { get; set; } = default!;

    }
}
