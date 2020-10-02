using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSP.App.Components;
using TSP.App.Services;
using TSP.Shared;

namespace TSP.App.Pages
{
    //[Authorize(Policy = Policies.CanManageContent)]
    public class SubSystemPageBase: ParentComponentBase
    {
        [Parameter]
        public int SubSystemId { get; set; }
        [Inject]
        SubMenuItemService Service { get; set; }
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }
        public IList<SubMenuItemModel> TabsModel { get; set; }
        
        protected override async Task OnParametersSetAsync()
        {
            await LoadTabs();
        }

        async Task LoadTabs()
        {
            try
            {
                TabsModel = await Service.GetAll<SubMenuItemModel>(SubSystemId, Token);
                var one = TabsModel.FirstOrDefault();
                if(one != null)
                {
                    one.TabHeader = "active";
                    one.TabDetail = "show active";
                    one.TabHeaderSelect = "true";
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            catch (Exception ex)
            {
                GlobalMsg.SetMessage(ex.Message, MessageLevel.Error);
            }
        }
        protected void TabClick(int tabId)
        {
            foreach (var tab in TabsModel)
            {
                if (tab.Id == tabId)
                {
                    tab.TabHeader = "active";
                    tab.TabDetail = "show active";
                    tab.TabHeaderSelect = "true";
                }
                else
                {
                    tab.TabHeader = "";
                    tab.TabDetail = "";
                    tab.TabHeaderSelect = "false";
                }
            }
        }
        
    }
}
