using ElasticPMTServer.Models;
using ElasticPMTServer.Models.DTO;
using System.Collections.Generic;

namespace ElasticPMTServer.Persistance
{
    public interface IRequirementRepository
    {
        List<RequirementDTO> GetAllRequirements();
        RequirementDTO GetRequirementById(int id);
        RequirementDTO AddRequirementToList(RequirementDTO requirement);
        bool DeleteRequirementFromList(int id);
    }
}
