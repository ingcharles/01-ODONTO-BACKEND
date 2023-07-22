using OdontoBackend.Aplication.Entities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Services.Contracts
{
    public interface IMailService
    {
        bool SendMail(MailData mailData);
    }
}
