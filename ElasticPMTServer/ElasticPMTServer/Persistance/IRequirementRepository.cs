using ElasticPMTServer.Models;
using ElasticPMTServer.Models.DTO;
using System.Collections.Generic;

namespace ElasticPMTServer.Persistance
{
    public interface IRequirementRepository
    {
        List<Requirement> GetAllRequirements();
        Requirement GetRequirementById(int id);
        Requirement CreateRequirement(RequirementDTO requirement);
        void DeleteRequirement(Requirement requirement);
        bool RequirementExists(int id);
    }
}
