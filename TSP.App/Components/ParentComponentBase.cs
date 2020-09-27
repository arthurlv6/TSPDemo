using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Threading.Tasks;

namespace TSP.App.Components
{
    public class ParentComponentBase : ComponentBase
    {
        //[Inject]
        //public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public string UserEmail { get; set; }
        [Inject]
        public GlobalMessage GlobalMsg { get; set; }
        [Inject]
        public IAccessTokenProvider AuthenticationService { get; set; }
        protected string Token { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var tokenResult = await AuthenticationService.RequestAccessToken();
            tokenResult.TryGetToken(out var tokenReference);
            Token = tokenReference.Value;

            //var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            //UserEmail = authState.User.Identity.Name;
            GlobalMsg.SetMessage();
        }
    }
}
