using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.CommonRoute.concrete;

namespace MerchantAcquirerAPI.Services
{
    public class AppBootstrapper
    {
        private static void AutoInjectLayers(IServiceCollection serviceCollection)
        {
            serviceCollection.Scan(scan => scan.FromCallingAssembly().AddClasses(classes => classes
                    .Where(type => type.Name.EndsWith("Repository") || type.Name.EndsWith("Service")), false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());

        }
        public static void InitServices(IServiceCollection services)
        {
            AutoInjectLayers(services);


            
            services.AddScoped<DbContext, MerchantAcquirerAPIAppContext>();
            services.AddTransient(typeof(DbContextOptions<MerchantAcquirerAPIAppContext>));
            services.AddTransient<IActivityLog, ActivityLogServices>(); 
            services.AddTransient<ICommonRoute, CommonRouteServices>();
          
        }



    }
}
