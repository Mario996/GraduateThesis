using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models.Pokusaj
{
    public class CustomControl
    {
        public string GroupTitle { get; set; }
        public string ControlId { get; set; }
        public string ControlClass { get; set; }
        public string ControlTitle { get; set; }
        public string PartId { get; set; }
        [Text(Analyzer = "ngram_analyzer", SearchAnalyzer = "ngram_analyzer")]
        public string PartProse { get; set; }
        [Completion(MaxInputLength = 5000, Analyzer = "ngram_analyzer", SearchAnalyzer = "ngram_analyzer")]
        public CompletionField Suggest { get; set; }



        public CustomControl(string groupTitle, string controlId, string controlClass, string controlTitle, string partId, string partProse)
        {
            this.GroupTitle = groupTitle;
            this.ControlId = controlId;
            this.ControlClass = controlClass;
            this.ControlTitle = controlTitle;
            this.PartId = partId;
            this.PartProse = partProse;
            this.Suggest = new CompletionField
            {
                Input = new[] { partProse }
            };
        }
    }
}
