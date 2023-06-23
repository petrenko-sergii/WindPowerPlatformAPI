using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WindPowerPlatformAPI.App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((context, config) =>
                {
                    var connectionString = context.Configuration.GetConnectionString("PostgreSqlConnection");

                    config.WriteTo.PostgreSQL(connectionString, "Logs", schemaName: "public", needAutoCreateTable: true, respectCase: true)
                        .MinimumLevel.Information();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
