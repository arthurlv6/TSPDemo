using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TSP.Shared;
using TSP.Funcs.Repos;

namespace TSP.Funcs
{
    public class FuncSubSystem
    {
        private readonly SubSystemRepo repo;
        public FuncSubSystem(SubSystemRepo _repo)
        {
            repo = _repo;
        }
        [FunctionName("SubSystem-all")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "getall")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Got all subsystems.");
            return new OkObjectResult(await repo.GetAll<SubSystem, SubSystemModel>());
        }
    }
}
