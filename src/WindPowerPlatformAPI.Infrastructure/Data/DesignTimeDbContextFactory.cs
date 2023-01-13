using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace WindPowerPlatformAPI.Infrastructure.Data
{
    //Summary:
    //EF calls CreateWebHostBuilder or BuildWebHost without running Main. So Iconfiguration is null.
    //Create new class, which inherited from IDesignTimeDbContextFactory
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CommandContext>
    {
        public CommandContext CreateDbContext(string[] args)
        {
            var wppApiProjFolder = "WindPowerPlatformAPI.App";
            var appSettingsFolder = Path.Combine(Directory.GetCurrentDirectory(), "..", wppApiProjFolder);
            
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(appSettingsFolder)
               .AddJsonFile("appsettings.json", optional: true)
               .Build();

            var optionsBuilder = new DbContextOptionsBuilder<CommandContext>();
            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("WindPowerPlatformAPI.Infrastructure"));

            return new CommandContext(optionsBuilder.Options);
        }
    }
}
