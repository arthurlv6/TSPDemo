using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSP.API.Repos;
using TSP.Shared;

namespace TSP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubSystemController : ControllerBase
    {
        private readonly SubSystemRepo repo;
        public SubSystemController(SubSystemRepo _repo)
        {
            repo = _repo;
        }
        // GET: api/Requirement
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = repo.GetAllAsync<SubSystem, SubSystemModel>().Result;
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return BadRequest(result.Error);
        }
    }
}
