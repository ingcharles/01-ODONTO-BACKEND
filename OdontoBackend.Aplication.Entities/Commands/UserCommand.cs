using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Commands
{
    public class UserCommand : AuditoriaCommand
    {

        public String ci { get; set; } = default!;
        public String names { get; set; } = default!;
        public String lastNames { get; set; } = default!;
        public String email { get; set; } = default!;
        public String password { get; set; } = default!;
        public Boolean licenseAgreement { get; set; } = default!;
        public Boolean? isProfesional { get; set; } = default!;
        public Boolean? isClinic { get; set; } = default!;
       
    }
}
