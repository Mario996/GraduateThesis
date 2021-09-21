using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Catalog
    {
        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }
    }
}
