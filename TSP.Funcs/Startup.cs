using AutoMapper;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using TSP.Funcs;
using TSP.Funcs.Repos;
using TSP.Shared.Mapper;

[assembly: FunctionsStartup(typeof(Startup))]
namespace TSP.Funcs
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            services.AddAutoMapper(typeof(ModelProfile).GetTypeInfo().Assembly);
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionString")), ServiceLifetime.Scoped);
            services.AddScoped<SubSystemRepo>();
            services.AddScoped<SubMenuItemRepo>();
            services.AddScoped<SubItemDetailRepo>();
            services.AddScoped<ContactUsRepo>();
            //services.AddSingleton<ImageStore>();

            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }
    }
}
