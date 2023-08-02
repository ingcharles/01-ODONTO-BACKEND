using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using OdontoBackend.Aplicacion.Mappers;
using OdontoBackend.Aplicacion.Mappers.Contracts;
using OdontoBackend.Aplicacion.Services;
using OdontoBackend.Aplicacion.Services.Contracts;

using OdontoBackend.Domain.Contracts;
using OdontoBackend.Infrastructure.Context;
using OdontoBackend.Infrastructure.Contracts.Context;
using OdontoBackend.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.CrossCutting.IoC
{
    public static class IocRegister
    {
        public static void RegisterServices(IServiceCollection services)
        {


            #region Application Layer
            //services.AddScoped<ICiudadanoMapper, CiudadanoMapper>();
            //services.AddScoped<IValidator<CiudadanoByIdQuery>, CiudadanoQueryValidator>();
            //services.AddScoped<IValidator<CiudadanoByIdCommand>, CiudadanoCommandValidator>();
            //services.AddScoped<ICiudadanoAppService, CiudadanoAppService>();
            //services.AddScoped<IPersonaMapper, PersonaMapper>();
            //services.AddScoped<IValidator<PersonaByCiQuery>, PersonaQueryValidator>();
            //services.AddScoped<IValidator<PersonaByIdCommand>, PersonaCommandValidator>();
            //services.AddScoped<IPersonaAppService, PersonaAppService>();
            ////services.AddScoped<IValidator<CatalogoCommand>, CatalogoCommandValidator>();
            services.AddScoped<ICatalogoMapper, CatalogoMapper>();
            services.AddScoped<ICatalogoService, CatalogoService>();
            services.AddScoped<IUserMapper, UserMapper>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMailService, MailService>();
                services.AddScoped<ITokenService, TokenService>();
          

            //services.AddScoped<IHttpClientFactory>();

            #endregion

            #region Domain-Infrastructure Layer
            services.AddScoped<IContextDatabase, ContextDatabase>();

            //services.AddScoped<ICiudadanoRepository, CiudadanoRepository>();
            services.AddScoped<ICatalogoRepository, CatalogoRepository>();
            services.AddScoped<IUserRepository, AuthRepository>();

            #endregion
        }
    }
}
