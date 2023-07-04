using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WindPowerPlatformAPI.App.Middleware;
using WindPowerPlatformAPI.Infrastructure.Data;
using WindPowerPlatformAPI.Infrastructure.DI;
using Npgsql;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.Logging;
using WindPowerPlatformAPI.Infrastructure.Attributes;

namespace WindPowerPlatformAPI.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new NpgsqlConnectionStringBuilder();
            builder.ConnectionString = Configuration.GetConnectionString("PostgreSqlConnection");
            builder.Username = Configuration["UserID"];
            builder.Password = Configuration["Password"];

            services.AddDbContext<ApplicationDbContext>(opt =>
                        opt.UseNpgsql(builder.ConnectionString,
                        b => b.MigrationsAssembly("WindPowerPlatformAPI.Infrastructure")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.Audience = Configuration["ResourceId"];
                    opt.Authority = $"{Configuration["Instance"]}{Configuration["TenantId"]}";
                });

            services.AddSingleton<BlockAnonymousMiddleware>();
            services.AddSingleton<SecurityHeadersMiddleware>();
            services.AddSingleton<BlockCrossSiteScriptingMiddleware>();

            services.AddCors(options => options
                .AddDefaultPolicy(
                    builder => builder
                        .WithOrigins(Configuration["AllowedOrigins"]?
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(x => x.Trim())
                            .ToArray() ?? new string[0])
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()));

            services.AddScoped<ValidationPostPutFilterAttribute>();

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddSwaggerGen();

            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.ConfigureDependencyInjection(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            ApplicationDbContext context,
            ILogger<Startup> logger)
        {
            LogInitialInfo(env, logger);

            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{AppDomain.CurrentDomain.FriendlyName} V1");
                });
            }
            else
            {
                app.UseMiddleware<BlockAnonymousMiddleware>();
            }
            app.UseCors();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<BlockCrossSiteScriptingMiddleware>();
            app.UseMiddleware<CorrelationIdLogContextMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<LoggingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void LogInitialInfo(IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("Application name: {0}", env.ApplicationName);
            logger.LogInformation("Assembly version: {0}", GetType().Assembly.GetName().Version);
            logger.LogInformation("Environment name: {0}", env.EnvironmentName);
            logger.LogInformation("Machine name: {0}", Environment.MachineName);
            logger.LogInformation("User name: {0}", Environment.UserName);
            logger.LogInformation("Current directory: {0}", Environment.CurrentDirectory);
        }
    }
}
