using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Commands
{
    public class CatalogoCommand : AuditoriaCommand
    {

        [DisplayName("Código Padre")]
        public Int64? codigoPadre { get; set; } = default!;
        //[DisplayName("Descripción")]
        //public string descripcion { get; set; } = default!;
        //[DisplayName("Código Cuenta")]
        //public string? numeroCuenta { get; set; } = default!;
        //[DisplayName("Estado")]
        //public int estado { get; set; } = default!;
    }
}
