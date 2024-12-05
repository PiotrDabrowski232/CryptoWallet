using CryptoWallet.Logic.Services;
using System.Reflection;

namespace CryptoWallet.DiConfig
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection WithServices(this IServiceCollection services)
        {
            var assemblies = Assembly.Load("CryptoWallet.Logic");

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assemblies));

            services.AddScoped<IWalletService, WalletService>();

            return services;
        }
    }
}
