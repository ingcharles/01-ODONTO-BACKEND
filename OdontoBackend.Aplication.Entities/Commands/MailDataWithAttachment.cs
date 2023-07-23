using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplication.Entities.Commands
{
    public class MailDataWithAttachment : MailData
    {
        public IFormFileCollection EmailAttachments { get; set; }
    }
}
