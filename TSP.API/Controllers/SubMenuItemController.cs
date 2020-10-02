using Microsoft.AspNetCore.Mvc;
using TSP.Shared;
using System.Threading.Tasks;
using TSP.API.Repos;
using Microsoft.AspNetCore.Authorization;

namespace TSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenuItemController : ControllerBase
    {
        private readonly SubMenuItemRepo repo;
        public SubMenuItemController(SubMenuItemRepo _repo)
        {
            repo = _repo;
        }
        // GET: api/Requirement
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await repo.GetAllAsync<SubMenuItem, SubMenuItemModel>();
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }

        [HttpGet("{subSystemId}")]
        public async Task<IActionResult> GetAll(int subSystemId = 1)
        {
            var result = await repo.GetAllAsync<SubMenuItemModel>(subSystemId);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Error);
        }
    }
}
