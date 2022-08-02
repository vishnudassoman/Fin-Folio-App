using FinFolio.PortFolioCore.Interfaces;
using FinFolio.PortFolioCore.Services;
using FinFolio.PortFolioRepository;
using FinFolio.PortFolioRepository.Interfaces;
using FinFolio.PortFolioRepository.Repository;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(FinFolio.PortFolio.WebAPI.Startup))]
namespace FinFolio.PortFolio.WebAPI
{
    public class Startup : FunctionsStartup
    {
        private IConfiguration _configuration;


        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            _configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
            builder.Services.AddDbContext<PortFolioDBContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("PortFolioDBConnection")
            )
            );
            //Repository Dependency
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<ISchemeRepository, SchemeRepository>();
            //Core Service Dependency
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            builder.Services.AddScoped<ISchemeService, SchemeService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);
        }
    }
}

