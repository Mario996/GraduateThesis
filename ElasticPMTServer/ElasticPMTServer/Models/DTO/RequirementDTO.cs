using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models.DTO
{
    public class RequirementDTO
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; }
        public string ControlId { get; set; }
        public string ControlClass { get; set; }
        public string ControlTitle { get; set; }
        public string PartId { get; set; }
        public string PartProse { get; set; }

        public RequirementDTO() { }

        public RequirementDTO(Requirement requirement)
        {
            this.Id = requirement.Id;
            this.GroupTitle = requirement.GroupTitle;
            this.ControlId = requirement.ControlId;
            this.ControlClass = requirement.ControlClass;
            this.ControlTitle = requirement.ControlTitle;
            this.PartId = requirement.PartId;
            this.PartProse = requirement.PartProse;
        }
    }
}
