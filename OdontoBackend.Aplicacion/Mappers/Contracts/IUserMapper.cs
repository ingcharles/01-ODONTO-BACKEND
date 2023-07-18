using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Mappers.Contracts
{
    public interface IUserMapper
    {
        public IQueryable<User> UserQueryToUserByCiPas(Task<UserByCiPasQuery> source);
        public IQueryable<UserViewModel> UserQueryFromUserByCiPas(Task<User> source);
    }
}
