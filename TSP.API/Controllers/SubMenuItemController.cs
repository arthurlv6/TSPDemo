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
        public IActionResult GetAll()
        {
            var temp = repo.GetAll<SubMenuItem, SubMenuItemModel>();
            return Ok(temp);
        }

        [HttpGet("{subSystemId}")]
        public async Task<IActionResult> GetAll(int subSystemId = 1)
        {
            var temp = await repo.GetAll<SubMenuItemModel>(subSystemId);
            return Ok(temp);
        }
    }
}
