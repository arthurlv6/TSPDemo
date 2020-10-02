using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.App.MessageHandlers
{
    public class TSPApiAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        private readonly IConfiguration _configuration;

        public TSPApiAuthorizationMessageHandler(
            IAccessTokenProvider provider, NavigationManager navigation, IConfiguration configuration) 
            : base(provider, navigation)
        {
            _configuration = configuration;
            ConfigureHandler(
                  authorizedUrls: new[] { _configuration["APIServerUri"] });
            //_configuration["APIServerUri"] 

        }
    }
}
