using OdontoBackend.Aplicacion.ViewModels.User;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Aplication.Entities.Queries.User;
using OdontoBackend.Domain.Models;
using OdontoBackend.Domain.Models.User;
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
        public IQueryable<AplicacionViewModel> AplicacionQueryFromAplicacionByCodUser(Task<List<OdontoBackend.Domain.Models.User.Aplicacion>> source);
        public IQueryable<User> UserCommandToRegisterUser(Task<UserCommand> source);

        public IQueryable<UserViewModel> UserCommandFromRegisterUser(Task<UserCommandFrom> source);
        public IQueryable<User> UserCommandToUpdateTokens(Task<UserViewModel> source);
        public IQueryable<User> UserCommandFromUpdateTokens(Task<User> source);
        public IQueryable<User> UserQueryToUserByCod(Task<UserByCodQuery> source);

        public IQueryable<UserViewModel> UserQueryFromUserByCod(Task<User> source);

        public IQueryable<Menu> MenuQueryToMenuByCodAplicacion(Task<MenuByCodAplicacionQuery> source);
        public IQueryable<MenuViewModel> MenuQueryFromMenuByCodAplicacion(Task<List<Menu>> source);

    }
}
