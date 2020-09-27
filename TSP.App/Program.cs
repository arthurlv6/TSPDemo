using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BethanysPieShopHRM.App.MessageHandlers;
using TSP.App.Services;
using TSP.App.Components;

namespace TSP.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services
                .AddTransient<TSPApiAuthorizationMessageHandler>();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("OidcConfiguration", options.ProviderOptions);
                builder.Configuration.Bind("UserOptions", options.UserOptions);
            });

            builder.Services.AddAuthorizationCore(authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    TSP.Shared.Policies.CanManageContent,
                    TSP.Shared.Policies.CanManageContentPolicy());
            });

            builder.Services.AddHttpClient<SubSystemService>(
                client => client.BaseAddress = new Uri("https://localhost:44383/")) //API
                .AddHttpMessageHandler<TSPApiAuthorizationMessageHandler>();
            
            builder.Services.AddHttpClient<SubMenuItemService>(
                client => client.BaseAddress = new Uri("https://localhost:44383/"))
                .AddHttpMessageHandler<TSPApiAuthorizationMessageHandler>();

            builder.Services.AddHttpClient<SubItemDetailService>(
                client => client.BaseAddress = new Uri("https://localhost:44383/"))
                .AddHttpMessageHandler<TSPApiAuthorizationMessageHandler>();

            builder.Services.AddScoped<GlobalMessage>();
            await builder.Build().RunAsync();
        }
    }
}
