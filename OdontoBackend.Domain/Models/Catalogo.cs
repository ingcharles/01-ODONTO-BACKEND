using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Models
{
    public class Catalogo : Auditoria
    {
        public Int64 cod_catalogo { get; set; } = default!;
        public Int64 cod_uni_catalogo { get; set; } = default!;
        //public Int64? COD_PAD_CATALOGO { get; set; } = default!;
        //public string DES_CATALOGO { get; set; } = default!;
        //public string? NUM_PLA_CUENTA { get; set; } = default!;
        //public int? EST_CATALOGO { get; set; } = default!;
        //public DateTime? FEC_CREACION { get; set; } = default!;
        //public DateTime? FEC_ACTUALIZACION { get; set; } = default!;
        //public string MENSAJE_LOGICA { get; set; } = default!;

    }
}
