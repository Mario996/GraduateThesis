using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models.ElasticSearch
{
    public class Param
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("label")]
        public string Label { get; set; }
    }
}
