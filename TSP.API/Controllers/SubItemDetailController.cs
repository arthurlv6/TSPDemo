using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;
using System.Threading.Tasks;
using TSP.Shared;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using System;
using TSPServer.Services;
using Microsoft.Extensions.Configuration;
using TSP.API.Repos;

namespace TSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubItemDetailController : ControllerBase
    {
        private readonly SubItemDetailRepo repo;
        private readonly IWebHostEnvironment env;
        private readonly ImageStore imageStore;
        private readonly IConfiguration configuration;
        public SubItemDetailController(
            SubItemDetailRepo _repo, 
            IWebHostEnvironment _env, 
            ImageStore _imageStore,
            IConfiguration _configuration)
        {
            repo = _repo;
            env = _env;
            imageStore = _imageStore;
            configuration = _configuration;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int submenuItemId = 0, int page = 1, int size = 20, string keyword = "")
        {
            if (page < 1) return BadRequest("page can't be negative.");
            if (size < 1) return BadRequest("Page size can't be negative");

            var temp = await repo.GetPageDataAsync<SubItemDetailModel>(submenuItemId, page, size, keyword);
            if (temp.IsSuccess)
            {
                HttpContext.InsertPaginationParameterInResponse(temp.Value.Item2);
                return Ok(temp.Value.Item1);
            }
                return BadRequest(temp.Error);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdate(int id, [FromBody] JsonPatchDocument<SubItemDetailModel> model)
        {
            if (id == 0)
                return BadRequest();

            var record = await repo.GetOneAsync<SubItemDetail, SubItemDetailModel>(id);
            if (record.Value.Id==0)
                return NotFound();

            model.ApplyTo(record.Value, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var maybe=await repo.UpdateAsync<SubItemDetail, SubItemDetailModel>(record.Value);
            if(maybe.IsSuccess)
                return NoContent();
            return BadRequest(maybe.Error);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> UploadFileAsync([FromForm] UploadProductLinkModel model)
        {
            if (model == null)
                return BadRequest();

            if (model.Id == 0)
            {
                ModelState.AddModelError("ProductId", "ProductId shouldn't be zero");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //
            string sasUrl=null;
            if (model.File != null)
            {
                    using (var stream = model.File.OpenReadStream())
                    {
                        var imageId = await imageStore.SaveImage(stream);
                        sasUrl = imageStore.UriFor(imageId);
                    }
            }
            await repo.UpdateImageAsync(model.Id, sasUrl);
            return Ok(sasUrl);
        }
        [HttpGet("image/{id}")]
        public async Task<IActionResult> GetAsync(int id) // for normal hosting.
        {
            var noPhoto = PhysicalFile(env.ContentRootPath + "\\uploaded\\no-photo.png", "image/jpeg");
            var temp = await repo.GetOneAsync<SubItemDetail, SubItemDetailModel>(id);
            if (temp.Value.Id == 0)
            {
                return noPhoto;
            }
            if (string.IsNullOrEmpty(temp.Value.Image))
            {
                return noPhoto;
            }
            var file = env.ContentRootPath + "\\uploaded\\" + temp.Value.Image;
            if (!System.IO.File.Exists(file))
            {
                return noPhoto;
            }
            return PhysicalFile(env.ContentRootPath + "\\uploaded\\" + temp.Value.Image, "image/jpeg");
        }

        [HttpPost("detail")]
        public IActionResult Post([FromBody] AddDetailModel model)
        {
            if (model == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            SubItemDetailModel subItemDetailModel = new SubItemDetailModel()
            {
                Name = model.Name,
                SubMenuItemId = model.MenuId
            };

            var result = repo.Add(subItemDetailModel);
            if (result.IsSuccess)
                return Created("SubItemDetail", new AddDetailModel() { Id = result.Value.Id, MenuId = result.Value.SubMenuItemId, Name = result.Value.Name });
            return BadRequest(result.Error);
        }
    }
}
