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
        private readonly IRepository _repository;
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService, IRepository repository)
        {
            _searchService = searchService;
            _repository = repository;
        }

        [HttpPost]
        public IActionResult Search([FromBody] SearchObject searchValue)
        {
            var result = _searchService.Autocomplete(searchValue.Query, 5);
            return Ok(result);
        }

        [HttpPost("create-index")]
        public IActionResult CreateIndex()
        {
            var result = _repository.create();
            return Ok(result);
        }

    }
}
