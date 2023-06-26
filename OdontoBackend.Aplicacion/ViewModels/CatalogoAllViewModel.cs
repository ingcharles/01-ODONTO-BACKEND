using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.ViewModels
{
    public class CatalogoAllViewModel
    {
        [DisplayName("Código")]
        public Int64 codigoCatalogo { get; set; } = default!;
    }
}
