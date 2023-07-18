using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Aplicacion.Mappers
{
    public class UserMapper : IUserMapper
    {
        public IQueryable<User> UserQueryToUserByCiPas(Task<UserByCiPasQuery> source)
        {
            return new List<User>
            {
                new User
                {
                   // COD_UNI_User = source.Result.codigoUnico
                    dni_usuario = source.Result.ci,
                    pas_usuario = source.Result.password
                }
            }.AsQueryable();
        }
        public IQueryable<UserViewModel> UserQueryFromUserByCiPas(Task<User> source)
        {
            return new List<UserViewModel>
            {
                new UserViewModel
                {
                    codigo = source.Result.cod_usuario


                }
            }.AsQueryable();
        }
    }
}
