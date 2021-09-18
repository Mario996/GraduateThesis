using ElasticPMTServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IIndexService _indexService;

        public IndexController(IIndexService indexService)
        {
            _indexService = indexService;
        }

        [HttpPost]
        public IActionResult CreateIndex()
        {
            return Ok(_indexService.populateIndex());
        }
    }
}
