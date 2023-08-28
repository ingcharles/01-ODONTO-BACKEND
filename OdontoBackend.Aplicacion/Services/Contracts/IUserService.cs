using OdontoBackend.Aplicacion.ViewModels.User;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Aplication.Entities.Queries.User;
using OdontoBackend.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Services.Contracts
{
    public interface IUserService : IDisposable
    {
        Task<IQueryable<UserViewModel>> GetUserByCiPas(Task<UserByCiPasQuery> request);
        Task<IQueryable<AplicacionViewModel>> GetAplicacionByCodUser(Task<UserByCodQuery> request);

        Task<IQueryable<UserViewModel>> SaveRegisterUser(Task<UserCommand> request);

        Task<IQueryable<UserViewModel>> GetUserByCod(Task<UserByCodQuery> request);

        Task<IQueryable<User>> UpdateTokensCodUsuario(Task<UserViewModel> request);

    }
}
