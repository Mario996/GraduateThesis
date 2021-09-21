using ElasticPMTServer.Models;
using ElasticPMTServer.Models.DTO;
using ElasticPMTServer.Persistance;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ElasticPMTServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RequirementsController : ControllerBase
    {
        private readonly IRequirementRepository _requirementRepository;

        public RequirementsController(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }


        [HttpGet]
        public IActionResult GetAllRequirements()
        {
            return Ok(_requirementRepository.GetAllRequirements());
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRequirement(int id)
        {
            return Ok(_requirementRepository.GetRequirementById(id));
        }

        [HttpPost]
        public IActionResult CreateRequirement(RequirementDTO dto)
        {
            var entity = _requirementRepository.AddRequirementToList(dto);
            return CreatedAtAction(nameof(GetRequirement), new { id = entity.Id }, entity);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRequirement(int id)
        {
            var result = _requirementRepository.DeleteRequirementFromList(id);
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
