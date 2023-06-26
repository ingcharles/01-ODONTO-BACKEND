using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Queries
{
    public class CatalogoByCodUniQuery
    {
        [DisplayName("Código Único")]
        public Int64 codigoUnico { get; set; } = default!;
    }
}
