using DataAccesLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GaleriAcıkArttırma.Extensions
{
    public static class PersistenceExtensionLayer
    {
        public static IServiceCollection AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration) 
        {

            #region Context
            services.AddDbContext<DataAccesLayer.Context.ApplicationDbContext>(options => { options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); });
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<DataAccesLayer.Context.ApplicationDbContext>();
            #endregion
            return services;
        }

    }
}
