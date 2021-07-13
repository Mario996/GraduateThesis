using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models.Pokusaj
{
    public class Root
    {
        [JsonPropertyName("catalog")]
        public Catalog Catalog { get; set; }
    }
}
