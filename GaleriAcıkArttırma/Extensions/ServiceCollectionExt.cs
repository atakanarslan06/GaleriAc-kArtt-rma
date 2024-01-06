using BusinessLayer.Abstraction;
using BusinessLayer.Concrete;
using Core.Models;

namespace GaleriAcıkArttırma.Extensions
{
    public static class ServiceCollectionExt
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
        {
            #region services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped(typeof(ApiResponse));
            #endregion
            return services;
        }
    }
}
