using OdontoBackend.Aplicacion.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models.User
{
    public partial class User : Auditoria
    {

        public User()

        {
        }
        public User(List<RefreshToken> refreshTokens)

        {

            refresh_tokens = refreshTokens;
            //Tasks = new HashSet<Task>();


        }

        public long? cod_usuario { get; set; } = default!;
        public string dni_usuario { get; set; } = default!;

        public string pas_usuario { get; set; } = default!;
        public string nom_usuario { get; set; } = default!;
        public string ape_usuario { get; set; } = default!;
        public bool lic_agr_usuario { get; set; } = default!;
        public string mai_usuario { get; set; } = default!;
        public bool? is_pro_usuario { get; set; } = default!;
        public bool? is_cli_usuario { get; set; } = default!;
        public string mensaje_logica { get; set; } = default!;
        public List<RefreshToken> refresh_tokens { get; set; } = default!;
    }
}
