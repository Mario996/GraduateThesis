using ElasticPMTServer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public interface IRequirementService
    {
        List<RequirementDTO> GetAllRequirements();
        RequirementDTO GetRequirementById(int id);
        RequirementDTO AddRequirementToList(RequirementDTO requirement);
        bool DeleteRequirementFromList(int id);
    }
}
