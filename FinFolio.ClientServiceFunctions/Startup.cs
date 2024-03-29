﻿using FinFolio.PortFolioCore.Interfaces;
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
            //builder.Services.AddApplicationInsightsTelemetry();
            //Repository Dependency
            builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
            builder.Services.AddScoped<ISchemeRepository, SchemeRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            //Core Service Dependency
            builder.Services.AddScoped<IWishlistService, WishlistService>();
            builder.Services.AddScoped<ISchemeService, SchemeService>();
            builder.Services.AddScoped<IUserService, UserService>();
        }

        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            base.ConfigureAppConfiguration(builder);
        }
    }
}

