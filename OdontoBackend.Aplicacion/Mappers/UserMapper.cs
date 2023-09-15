using OdontoBackend.Aplicacion.Mappers.Contracts;
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
                    nombreUsuario = source.Result.nom_usuario,
                    mensajeLogica = source.Result.mensaje_logica


                }
            }.AsQueryable();
        }

        public IQueryable<AplicacionViewModel> AplicacionQueryFromAplicacionByCodUser(Task<List<OdontoBackend.Domain.Models.User.Aplicacion>> source)
        {
            var aplicacionList = source.Result;
            var aplicacion = new List<AplicacionViewModel>();
            foreach (var item in aplicacionList)
            {
                aplicacion.Add(new AplicacionViewModel
                {
                    codigo = item.cod_aplicacion,
                    nombre = item.nom_aplicacion,
                    descripcion = item.des_aplicacion,
                    icono = item.ico_aplicacion,
                    color = item.col_aplicacion,
                    nemonico = item.nmo_aplicacion,
                    estado = item.est_aplicacion,
                    mensajeLogica = item.mensaje_logica,
                    codigoUsuario = item.cod_usuario

                });
            }
            return aplicacion.AsQueryable();

            
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

        public IQueryable<User> UserCommandToUpdateTokens(Task<UserViewModel> source)
        {
            return new List<User>
            {
                new User
                {
                    cod_usuario = source.Result.codigoUsuario,
                    refresh_tokens = source.Result.refreshTokens,



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

        public IQueryable<User> UserQueryToUserByCod(Task<UserByCodQuery> source)
        {
            return new List<User>
            {
                new User
                {
                    cod_usuario = source.Result.codigoUsuario,
                }
            }.AsQueryable();
        }
        public IQueryable<UserViewModel> UserQueryFromUserByCod(Task<User> source)
        {
            return new List<UserViewModel>
            {
                new UserViewModel
                {
                    codigoUsuario = source.Result.cod_usuario,
                    nombreUsuario = source.Result.nom_usuario,
                    refreshTokens = source.Result.refresh_tokens,
                    mensajeLogica = source.Result.mensaje_logica


                }
            }.AsQueryable();
        }

        public IQueryable<Menu> MenuQueryToMenuByCodAplicacion(Task<MenuByCodAplicacionQuery> source)
        {
            return new List<Menu>
            {
                new Menu
                {
                    cod_aplicacion = source.Result.codigoUsuario,
                    cod_usuario = source.Result.codigoAplicacion
                }
            }.AsQueryable();
        }

        public IQueryable<MenuViewModel> MenuQueryFromMenuByCodAplicacion(Task<List<Menu>> source)
        {
            var aplicacionList = source.Result;
            var aplicacion = new List<MenuViewModel>();
            foreach (var item in aplicacionList)
            {
                aplicacion.Add(new MenuViewModel
                {
                    codigo = item.cod_menu,
                    nombre = item.nom_menu,
                    descripcion = item.des_menu,
                    icono = item.ico_menu,
                    link = item.lin_menu,
                    codigoPadre = item.cod_menu_padre,
                    estado = item.est_menu,
                    mensajeLogica = item.mensaje_logica

                });
            }
            return aplicacion.AsQueryable();

        }
    }
}
