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

namespace TSP.Funcs
{
    public class FuncSubMenuItem
    {
        private readonly SubMenuItemRepo repo;
        public FuncSubMenuItem(SubMenuItemRepo _repo)
        {
            repo = _repo;
        }
        [FunctionName("SubMenuItem-all")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SubMenuItems")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Got sub ment items");

            return new OkObjectResult(await repo.GetAll<SubMenuItem, SubMenuItemModel>());
        }
        [FunctionName("SubMenuItem-get-one-by-Id")]
        public async Task<IActionResult> GetSubMenuItemById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SubMenuItem/{id}")] HttpRequest req,
            ILogger log, int id)
        {
            if (id < 0) return new BadRequestObjectResult("Id is not valid.");
            log.LogInformation($"Got sub ment items by id {id}");
            return new OkObjectResult(await repo.GetOneAsync<SubMenuItem,SubMenuItemModel>(id));
        }

        [FunctionName("SubMenuItem-get-all-by-subsystemId")]
        public async Task<IActionResult> GetSubMenuItemsBySubId(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "SubMenuItems/{subsystemId}")] HttpRequest req,ILogger log, int subsystemId)
        {
            log.LogInformation($"Got sub ment items by id {subsystemId}");
            return new OkObjectResult(await repo.GetAll< SubMenuItemModel>(subsystemId));
        }

        
    }
}
