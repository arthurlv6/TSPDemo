using AutoMapper;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using TSP.API.Repos;
using TSP.Shared.Mapper;
using TSPServer.Services;

namespace TSP.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            // Add an authorization policy
            services.AddAuthorization(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    Shared.Policies.CanManageContent,
                    Shared.Policies.CanManageContentPolicy());
            });


            //services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase(databaseName: "BethanysPieShopHRM"));
            var requireAuthenticatedUserPolicy = new AuthorizationPolicyBuilder()
                 .RequireAuthenticatedUser()
                 .Build();

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "https://tsp-dev-idp-server.azurewebsites.net/"; //IDP
                options.ApiName = "tspapi";
            });
            //services.AddAuthentication()
            //    .AddIdentityServerJwt();

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddAutoMapper(typeof(ModelProfile).GetTypeInfo().Assembly);
            services.AddScoped<SubSystemRepo>();
            services.AddScoped<SubMenuItemRepo>();
            services.AddScoped<SubItemDetailRepo>();
            services.AddScoped<ContactUsRepo>();
            services.AddSingleton<ImageStore>();

            services.AddControllers(configure =>
            configure.Filters.Add(new AuthorizeFilter(requireAuthenticatedUserPolicy)));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("store-v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Store",
                    Version = "1.0.1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Open");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/store-v1/swagger.json", "store");
            });
        }
    }
}
