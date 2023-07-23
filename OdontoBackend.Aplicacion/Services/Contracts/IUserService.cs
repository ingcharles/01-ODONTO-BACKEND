﻿using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Commands;
using OdontoBackend.Aplication.Entities.Queries;
using OdontoBackend.Domain.Models;
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

        Task<IQueryable<UserViewModel>> SaveRegisterUser(Task<UserCommand> request);

            Task<IQueryable<User>> GetTokensCodUsuario(Task<UserByCodUsuarioQuery> request);

        Task<IQueryable<User>> UpdateTokensCodUsuario(Task<User> request);

    }
}
