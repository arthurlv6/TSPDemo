using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.JSInterop;
using System.Threading.Tasks;
using TSP.App.Services;
using TSP.Shared;

namespace TSP.App.Components
{
    [Authorize]
    public class SubSystemItemBase: ParentComponentBase
    {
        [Inject]
        SubItemDetailService Service { get; set; }
        
        [Parameter]
        public int SubMenuItemId { get; set; }

        public PageDataModel<SubItemDetailModel> PageDataModel { get; set; }
        protected int currentPage = 1;
        protected string title = string.Empty;
        protected string categoryId = "0";
        protected string Name { get; set; }
        protected async Task Search(ChangeEventArgs e)
        {
            title = e.Value.ToString();
            currentPage = 1;
            await LoadDetails();
        }
        protected override async Task OnParametersSetAsync()
        {
            await LoadDetails();
        }
        protected async Task SelectedPage(int page)
        {
            currentPage = page;
            await LoadDetails(page);
        }

        protected async Task Filter()
        {
            currentPage = 1;
            await LoadDetails();
        }
        protected async Task Add()
        {
            try
            {
                var detailModel = await Service.Add(new AddDetailModel() { MenuId= SubMenuItemId, Name=Name }, Token);
                if (detailModel == null)
                {
                    GlobalMsg.SetMessage("Failed to save", MessageLevel.Error);
                }
                else
                {
                    // workaround to refresh data
                    await Filter();
                    GlobalMsg.SetMessage("New line added", MessageLevel.Normal);
                }
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            catch (System.Exception ex)
            {
                GlobalMsg.SetMessage(ex.Message, MessageLevel.Error);
            }
        }
        async Task LoadDetails(int page = 1, int quantityPerPage = 10)
        {
            try
            {
                PageDataModel = await Service.GetSubItemDetailAsync(SubMenuItemId.ToString(), page, quantityPerPage, title, Token);
            }
            catch (AccessTokenNotAvailableException exception)
            {
                exception.Redirect();
            }
            catch (System.Exception ex)
            {
                GlobalMsg.SetMessage(ex.Message, MessageLevel.Error);
            }
        }
        
    }
}
