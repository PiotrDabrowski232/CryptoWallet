using CryptoWallet.Logic.Services;
using CryptoWallet.Logic.Services.Interfaces;
using System.Reflection;

namespace CryptoWallet.DiConfig
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            var assemblies = Assembly.Load("CryptoWallet.Logic");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            services.AddScoped<IWalletValidationService, WalletValidationService>();
            services.AddScoped<IBinanceCommunicationService, BinanceCommunicationService>();
            services.AddHttpClient();
            return services;
        }
    }
}
