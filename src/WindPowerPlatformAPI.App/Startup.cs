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

namespace WindPowerPlatformAPI.App
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
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

            services.AddControllers().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.ConfigureDependencyInjection(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseMiddleware<BlockAnonymousMiddleware>();
            }

            app.UseRouting();

            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<BlockCrossSiteScriptingMiddleware>();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
