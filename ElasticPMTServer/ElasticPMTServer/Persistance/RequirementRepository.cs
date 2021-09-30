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

        public Requirement CreateRequirement(RequirementDTO dto)
        {
            Requirement newRequirement = new Requirement(dto);
            _context.Requirements.Add(newRequirement);
            _context.SaveChanges();
            return newRequirement;
        }

        public void DeleteRequirement(Requirement requirement)
        {
            _context.Requirements.Remove(requirement);
            _context.SaveChanges();
        }

        public List<Requirement> GetAllRequirements()
        {
            return _context.Requirements.ToList();
        }

        public Requirement GetRequirementById(int id)
        {
            return _context.Requirements.Where(requirement => requirement.Id == id).First();
        }

        public bool RequirementExists(int id)
        {
            return _context.Requirements.Where(requirement => requirement.Id == id).FirstOrDefault() != null;
        }
    }
}
