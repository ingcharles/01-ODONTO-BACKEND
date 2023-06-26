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
            //services.AddScoped<ICrcaMapper, CrcaMapper>();
            //services.AddScoped<ICrcaService, CrcaService>();
            //services.AddScoped<IIngresoMapper, IngresoMapper>();
            //services.AddScoped<IIngresoService, IngresoService>();
            //services.AddScoped<IGeneralMapper, GeneralMapper>();
            //services.AddScoped<IGeneralService, GeneralService>();
            //services.AddScoped<IPruebaMapper, PruebaMapper>();
            //services.AddScoped<IPruebaService, PruebaService>();
            //services.AddScoped<IActivoFijoMapper, ActivoFijoMapper>();
            //services.AddScoped<IActivoFijoService, ActivoFijoService>();
            #endregion

            #region Domain-Infrastructure Layer
            services.AddScoped<IContextDatabase, ContextDatabase>();
            //services.AddScoped<ICiudadanoRepository, CiudadanoRepository>();
            services.AddScoped<ICatalogoRepository, CatalogoRepository>();
            //services.AddScoped<IPersonaRepository, PersonaRepository>();
            //services.AddScoped<ICrcaRepository, CrcaRepository>();
            //services.AddScoped<IIngresoRepository, IngresoRepository>();
            //services.AddScoped<IGeneralRepository, GeneralRepository>();
            //services.AddScoped<IPruebaRepository, PruebaRepository>();
            //services.AddScoped<IActivoFijoRepository, ActivoFijoRepository>();

            #endregion
        }
    }
}
