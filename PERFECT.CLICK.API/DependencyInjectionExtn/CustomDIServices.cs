using Microsoft.Extensions.DependencyInjection;
using PERFECT.CLICK.DAL.UnitOfWork;
using PERFECT.CLICK.SERVICES.Interfaces;
using PERFECT.CLICK.SERVICES.Services;

namespace PERFECT.CLICK.API.DependencyInjectionExtn
{
    public static class CustomDIServices
    {
        public static void AddTransientServices(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        public static void AddScopedServices(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
        }
    }
}
