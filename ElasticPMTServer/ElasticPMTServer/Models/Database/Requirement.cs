using ElasticPMTServer.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Requirement
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; }
        public string ControlId { get; set; }
        public string ControlClass { get; set; }
        public string ControlTitle { get; set; }
        public string PartId { get; set; }
        public string PartProse { get; set; }

        public Requirement() { }

        public Requirement(RequirementDTO dto)
        {
            this.GroupTitle = dto.GroupTitle;
            this.ControlId = dto.ControlId;
            this.ControlClass = dto.ControlClass;
            this.ControlTitle = dto.ControlTitle;
            this.PartId = dto.PartId;
            this.PartProse = dto.PartProse;
        }
    }
}
