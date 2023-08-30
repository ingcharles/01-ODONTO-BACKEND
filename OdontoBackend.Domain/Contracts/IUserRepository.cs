using OdontoBackend.Domain.Models;
using OdontoBackend.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetUserByCiPas(User request);
        Task<IQueryable<OdontoBackend.Domain.Models.User.Aplicacion>> GetAplicacionByCodUser(User request);

        Task<IQueryable<UserCommandFrom>> SaveRegisterUser(User request);
        Task<IQueryable<User>> UpdateTokensCodUsuario(User request);
        Task<IQueryable<User>> GetUserByCod(User request);


    }
}
