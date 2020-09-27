using TSP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using TSP.API.Repos;

namespace TSP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactUsController : ControllerBase
    {
        private readonly ContactUsRepo repo;
        private readonly IWebHostEnvironment env;
        public ContactUsController(ContactUsRepo _repo, IWebHostEnvironment _env)
        {
            repo = _repo;
            env = _env;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ContactUsModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await repo.AddAsync(model);

            return Created("contactus", created);
        }
    }
}
