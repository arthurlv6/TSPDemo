using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TSP.Funcs.Repos;
using TSP.Shared;
using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace TSP.Funcs
{
    public class FuncSubItemDetail
    {
        private readonly SubItemDetailRepo repo;
        public FuncSubItemDetail(SubItemDetailRepo _repo)
        {
            repo = _repo;
        }
        [FunctionName("SubItem-Detail-getall")]
        public async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SubItemDetailGetAll")] HttpRequest req,
            ILogger log)
        {
            int submenuItemId = 0; int page = 1; int size = 20; string keyword = "";
            page = req.Query["page"].Count==1?int.Parse( req.Query["page"].ToString()):1;
            size = req.Query["size"].Count == 1 ? int.Parse(req.Query["size"].ToString()):20;
            submenuItemId = req.Query["submenuItemId"].Count == 1 ?  int.Parse(req.Query["submenuItemId"].ToString()):0;
            keyword = req.Query["keyword"].ToString();

            log.LogInformation("Got sub item details");
            if (page < 1) return new BadRequestObjectResult("page can't be negative.");
            if (size < 1) return new BadRequestObjectResult("Page size can't be negative");

            var temp = await repo.GetPageData<SubItemDetailModel>(submenuItemId, page, size, keyword);
            req.HttpContext.Response.Headers.Add("pagesQuantity", temp.Item2.ToString());
            return new OkObjectResult(temp.Item1);
        }


        [FunctionName("SubItem-Detail-GetOneById")]
        public async Task<IActionResult> GetItemDetailById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SubItemDetailGetOne/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            log.LogInformation($"Got sub item detail by id {id}");
            return new OkObjectResult(await repo.GetOneAsync<SubItemDetail,SubItemDetailModel>(id));
        }


        [FunctionName("SubItem-Detail-patch")]
        public async Task<IActionResult> PartiallyUpdate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "SubItemDetailPatch/{id}")] HttpRequest req, ILogger log, int id)
        {
            if (id < 0)
                return new BadRequestObjectResult("id cannot be smaller than zero");

            var record = await repo.GetOneAsync<SubItemDetail, SubItemDetailModel>(id);
            if (record == null)
                return new NotFoundObjectResult("Not found");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<JsonPatchDocument<SubItemDetailModel>>(requestBody);

            model.ApplyTo(record);

            await repo.UpdateAsync<SubItemDetail, SubItemDetailModel>(record);
            return new NoContentResult();
        }

        [FunctionName("SubItem-Detail-postAImage")]
        public async Task<IActionResult> PostAImage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SubItemDetailPostAImage")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<UploadProductLinkModel>(requestBody);

            if (model == null)
                return new BadRequestObjectResult("can't convert the input to a model");

            if (model.Id < 0)
            {
                return new BadRequestObjectResult("ProductId shouldn't be smaller than zero");
            }
            string sasUrl = null;
            if (model.File != null)
            {
                //var isAzure = configuration.GetValue<string>("IsAzure");
                //if (isAzure.ToUpper() == "YES")
                //{
                //    using (var stream = model.File.OpenReadStream())
                //    {
                //        var imageId = await imageStore.SaveImage(stream);
                //        sasUrl = imageStore.UriFor(imageId);
                //    }
                //}
            }
            await repo.UpdateImageAsync(model.Id, sasUrl);
            return new OkObjectResult(sasUrl);
        }

        [FunctionName("SubItem-Detail-Post")]
        public async Task<IActionResult> PostImage(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SubItemDetailPost")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var model = JsonConvert.DeserializeObject<AddDetailModel>(requestBody);

            if (model == null)
                return new BadRequestObjectResult("failed to convert the input.");


            SubItemDetailModel subItemDetailModel = new SubItemDetailModel()
            {
                Name = model.Name,
                SubMenuItemId = model.MenuId
            };

            var created = await repo.AddAsync(subItemDetailModel);

            return new CreatedResult("SubItemDetail", new AddDetailModel() { Id = created.Id, MenuId = created.SubMenuItemId, Name = created.Name });
        }

    }
}
