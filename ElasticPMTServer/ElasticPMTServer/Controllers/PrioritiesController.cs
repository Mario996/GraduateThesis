using ElasticPMTServer.Models;
using ElasticPMTServer.Repositories;
using ElasticPMTServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IPriorityRepository _priorityRepository;
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost("search")]
        public IActionResult Search([FromBody] SearchObject searchValue)
        {
            var result = _searchService.Autocomplete(searchValue.Query, 5);
            return Ok(result);
        }

    }
}
