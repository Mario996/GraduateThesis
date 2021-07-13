using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models.Pokusaj
{
    public class Catalog
    {
        [JsonPropertyName("groups")]
        public List<Group> Groups { get; set; }
    }
}
