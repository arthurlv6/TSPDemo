using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TSPWebsite.Services;

namespace TSPWebsite
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["api"]) });
            builder.Services.AddScoped<GlobalMessage>();
            builder.Services.AddTransient<SubItemDetailService>();
            builder.Services.AddTransient<CaptchaService>();
            builder.Services.AddTransient<ContactUsService>();

            await builder.Build().RunAsync();
        }
    }
}
