using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;

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

                    config
                        .WriteTo.PostgreSQL(connectionString, "Logs", schemaName: "public", needAutoCreateTable: true, respectCase: true)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .MinimumLevel.Information()
                        .Enrich.WithProperty("Version", typeof(Startup).Assembly.GetName().Version)
                        .Enrich.WithProperty("MachineName", Environment.MachineName);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
