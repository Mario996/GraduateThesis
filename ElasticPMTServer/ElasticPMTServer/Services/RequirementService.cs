using ElasticPMTServer.Models.DTO;
using ElasticPMTServer.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IRequirementRepository _requirementRepository;

        public RequirementService(IRequirementRepository requirementRepository)
        {
            _requirementRepository = requirementRepository;
        }

        public RequirementDTO AddRequirementToList(RequirementDTO requirement)
        {
            return new RequirementDTO(_requirementRepository.CreateRequirement(requirement));
        }

        public bool DeleteRequirementFromList(int id)
        {
            if (!_requirementRepository.RequirementExists(id))
            {
                return false;
            }

            _requirementRepository.DeleteRequirement(_requirementRepository.GetRequirementById(id));
            return true;
        }

        public List<RequirementDTO> GetAllRequirements()
        {
            return _requirementRepository.GetAllRequirements().Select(req => new RequirementDTO(req)).ToList();
        }

        public RequirementDTO GetRequirementById(int id)
        {
            if (_requirementRepository.RequirementExists(id))
            {
                return new RequirementDTO(_requirementRepository.GetRequirementById(id));
            }

            return null;
        }
    }
}
