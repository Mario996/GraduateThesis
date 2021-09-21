using ElasticPMTServer.Models;
using ElasticPMTServer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Persistance
{
    public class RequirementRepository : IRequirementRepository
    {
        private readonly DatabaseContext _context;
        
        public RequirementRepository(DatabaseContext context)
        {
            _context = context;
        }

        public RequirementDTO AddRequirementToList(RequirementDTO dto)
        {
            Requirement newRequirement = new Requirement(dto);
            _context.Requirements.Add(newRequirement);
            _context.SaveChanges();
            return new RequirementDTO(newRequirement);
        }

        public bool DeleteRequirementFromList(int id)
        {
            Requirement requirement = _context.Requirements.Where(req => req.Id == id).FirstOrDefault();
            if (requirement == null)
            {
                return false;
            }

            _context.Requirements.Remove(requirement);
            _context.SaveChanges();
            return true;
        }

        public List<RequirementDTO> GetAllRequirements()
        {
            return _context.Requirements.Select(req => new RequirementDTO(req)).ToList();
        }

        public RequirementDTO GetRequirementById(int id)
        {
            var result = _context.Requirements.FirstOrDefault(req => req.Id == id);
            if(result != null)
            {
                return new RequirementDTO(result);
            }

            return null;
        }
    }
}
