using ElasticPMTServer.Models;
using ElasticPMTServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpPost]
        public IActionResult Search([FromBody] SearchObject searchValue)
        {
            return Ok(_searchService.Autocomplete(searchValue.Query, 5));
        }
    }
}
