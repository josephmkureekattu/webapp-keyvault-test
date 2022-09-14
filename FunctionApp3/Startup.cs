using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(FunctionApp3.Startup))]
namespace FunctionApp3
{
    public class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var context = builder.GetContext();
            builder.ConfigurationBuilder.AddJsonFile(Path.Combine(context.ApplicationRootPath, "local.settings.json"), true, reloadOnChange: true);
            builder.ConfigurationBuilder.AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), false, reloadOnChange: true);
            builder.ConfigurationBuilder.AddEnvironmentVariables();
            var configuration = context.Configuration;
            
            var keyVaultEndpoint = new Uri($"https://{configuration["AzureKerVaultUrl"]}.vault.azure.net/");
            builder.ConfigurationBuilder.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());
            base.ConfigureAppConfiguration(builder);
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var context = builder.GetContext();
            builder.Services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(
                   b =>
                   {
                       b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                       b.CommandTimeout(1800);

                   })); // will be created in web project root
        }
    }
}
