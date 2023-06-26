using OdontoBackend.CrossCutting.IoC;

namespace OdontoBackend.Services.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            IocRegister.RegisterServices(services);
        }

    }
}
