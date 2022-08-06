using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MerchantAcquirerAPI.Data;
using MerchantAcquirerAPI.Services.AuditLog.Concrete;
using MerchantAcquirerAPI.Services.CommonRoute;
using MerchantAcquirerAPI.Services.CommonRoute.concrete;
using MerchantAcquirerAPI.Services.AccountType.Interface;
using MerchantAcquirerAPI.Services.AccountType.Concrete;
using MerchantAcquirerAPI.Services.BusinessCategory.Interface;
using MerchantAcquirerAPI.Services.Terminal.Concrete;
using MerchantAcquirerAPI.Services.Terminal.Interface;
using MerchantAcquirerAPI.Services.BusinessCategory.Concrete;
using MerchantAcquirerAPI.Services.CustomerRequest.Interface;
using MerchantAcquirerAPI.Services.CustomerRequest.Concrete;
using MerchantAcquirerAPI.Services.MccInformation.Interface;
using MerchantAcquirerAPI.Services.MccInformation.Concrete;
using MerchantAcquirerAPI.Services.State.Interface;
using MerchantAcquirerAPI.Services.State.Concrete;
using MerchantAcquirerAPI.Services.Network.Interface;
using MerchantAcquirerAPI.Services.Network.Concrete;
using MerchantAcquirerAPI.Services.FileHandler;
using MerchantAcquirerAPI.Services.Account.Interface;
using MerchantAcquirerAPI.Services.Account.Concrete;

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
            services.AddTransient< IAccountType, AccountTypeServices >();
            services.AddTransient<IBusinessCategory, BusinessOccupationsServices>();
            services.AddTransient<ITerminal, TerminalServices>();
            services.AddTransient<ICustomerRequest, CustomerRequestServices>();
            services.AddTransient<IMccInformation, MccInformationServices>();
            services.AddTransient<IState, StateServices>();
            services.AddTransient<INetwork, NetworkServices>();
            services.AddTransient<IFileHandler, FileHandlerServices>();
            services.AddTransient<IAccount, AccountServices>();
        }



    }
}
