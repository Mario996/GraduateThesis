using ElasticPMTServer.Models;
using ElasticPMTServer.Models.DTO;
using ElasticPMTServer.Persistance;
using ElasticPMTServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequirementsController : ControllerBase
    {
        private readonly IRequirementService _requirementService;

        public RequirementsController(IRequirementService requirementService)
        {
            _requirementService = requirementService;
        }


        [HttpGet]
        public IActionResult GetAllRequirements()
        {
            return Ok(_requirementService.GetAllRequirements());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRequirementById(int id)
        {
            return Ok(_requirementService.GetRequirementById(id));
        }

        [HttpPost]
        public IActionResult CreateRequirement(RequirementDTO dto)
        {
            var entity = _requirementService.AddRequirementToList(dto);
            return CreatedAtAction(nameof(GetRequirementById), new { id = entity.Id }, entity);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRequirementById(int id)
        {
            var result = _requirementService.DeleteRequirementFromList(id);
            if (result)
            {
                return Ok(result);
            } else
            {
                return NotFound();
            }
        }
    }
}
