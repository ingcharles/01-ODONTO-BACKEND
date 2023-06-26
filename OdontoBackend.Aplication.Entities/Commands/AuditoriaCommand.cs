using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Commands
{
    public class AuditoriaCommand
    {
        [DisplayName("Auditoria")]
        public string auditoria { get; set; } = default!;
    }
}
