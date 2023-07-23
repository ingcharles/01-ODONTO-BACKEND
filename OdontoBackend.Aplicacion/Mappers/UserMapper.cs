using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Aplicacion.ViewModels;
using OdontoBackend.Aplication.Entities.Commands;
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
                    codigoUsuario = source.Result.cod_usuario,
                    mensajeLogica = source.Result.mensaje_logica


                }
            }.AsQueryable();
        }
        public IQueryable<User> UserCommandToRegisterUser(Task<UserCommand> source)
        {
            return new List<User>
            {
                new User
                {
                    dni_usuario = source.Result.ci,
                    pas_usuario = source.Result.password,
                    mai_usuario = source.Result.email,
                    nom_usuario = source.Result.names,
                    ape_usuario = source.Result.lastNames,
                    lic_agr_usuario = source.Result.licenseAgreement,
                    is_pro_usuario = source.Result.isProfesional,
                    is_cli_usuario = source.Result.isClinic,


                }
            }.AsQueryable();
        }
        public IQueryable<UserViewModel> UserCommandFromRegisterUser(Task<UserCommandFrom> source)
        {
            return new List<UserViewModel>
            {
                new UserViewModel
                {
                    codigoUsuario = source.Result.cod_usuario,
                    mensajeLogica = source.Result.mensaje_logica


                }
            }.AsQueryable();
        }

        public IQueryable<User> UserCommandToUpdateTokens(Task<User> source)
        {
            return new List<User>
            {
                new User
                {
                    cod_usuario = source.Result.cod_usuario,
                    RefreshTokens = source.Result.RefreshTokens,



                }
            }.AsQueryable();
        }
        public IQueryable<User> UserCommandFromUpdateTokens(Task<User> source)
        {
            return new List<User>
            {
                new User
                {
                    cod_usuario = source.Result.cod_usuario,
                    mensaje_logica = source.Result.mensaje_logica


                }
            }.AsQueryable();
        }
       
    }
}
