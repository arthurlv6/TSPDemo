using BlazorInputFile;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TSP.App.Services;
using TSP.Shared;

namespace TSP.App.Components
{
    public class SubItemDetailLineBase: ParentComponentBase
    {
        [Parameter]
        public SubItemDetailModel Item { get; set; }
        [Inject]
        protected SubItemDetailService Service { get; set; }
        [Inject]
        IJSRuntime JSRuntime { get; set; }
        protected async Task ShowDetail(SubItemDetailModel model)
        {
            
            if (model.IsShowClass == "d-block")
            {
                Item.IsShowClass = "d-none";
            }
            else
            {
                Item.IsShowClass = "d-block";
                await JSRuntime.InvokeVoidAsync("blazorInterop.initializeSummernote",model.Id,model.Paragraph);
            }
        }
        protected async Task SaveChanges(int id)
        {
            string paragraph = await JSRuntime.InvokeAsync<string>("blazorInterop.initializeSummernoteGet",id);
            await Change( new ChangeEventArgs() { Value=paragraph },PatchUpdateItem.Paragraph);
        }
        protected async Task Change(ChangeEventArgs e, PatchUpdateItem patchUpdateItem)
        {
            var val = e.Value.ToString();
            PatchUpdate[] patchUpdates = new PatchUpdate[1];
            if (patchUpdateItem == PatchUpdateItem.Title)
            {
                patchUpdates[0] = new PatchUpdate { op = "replace", path = "Title", value = val };
                Item.Title = val;
            }
            if (patchUpdateItem == PatchUpdateItem.Paragraph)
            {
                patchUpdates[0] = new PatchUpdate { op = "replace", path = "Paragraph", value = val };
            }
            if (patchUpdateItem == PatchUpdateItem.Order)
            {
                patchUpdates[0] = new PatchUpdate { op = "replace", path = "Order", value = val };
            }
            if (patchUpdateItem == PatchUpdateItem.Disabled)
            {
                patchUpdates[0] = new PatchUpdate { op = "replace", path = "Disabled", value = val };
            }
            var isDone = await Service.UpdateAsync(Item.Id, val, patchUpdates);
            if (!isDone)
                GlobalMsg.SetMessage("Failed to change the name");
        }

        //file upload
        protected IFileListEntry[] selectedFiles;
        protected void HandleSelection(IFileListEntry[] files)
        {
            selectedFiles = files;
        }


        protected async Task LoadFile(IFileListEntry file)
        {
            if (!file.Type.Contains("image"))
            {
                GlobalMsg.SetMessage("Please upload image file(.png or .jpg).", MessageLevel.Error);
                return;
            };
            if (file.Size>1000000)
            {
                GlobalMsg.SetMessage("File should be smaller than 1M.", MessageLevel.Error);
                return;
            };

            file.OnDataRead += (sender, eventArgs) => InvokeAsync(StateHasChanged);
            var model = new UploadProductLinkModel()
            {
                Id = Item.Id,
                Image = file.Data,
                ImageName = file.Name
            };
            var imageUrl=await Service.PostImage(model, Token);
            if (imageUrl != null)
            {
                // show the image
                Item.Image = imageUrl;
                GlobalMsg.SetMessage("The file is uploaded.", MessageLevel.Normal);
            }
            else
            {
                GlobalMsg.SetMessage("Failed to upload the file.", MessageLevel.Error);
            }
        }
    }
}
