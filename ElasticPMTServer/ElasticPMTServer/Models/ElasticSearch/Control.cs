using ElasticPMTServer.Models.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models
{
    public class Control
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("params")]
        public List<Param> Params { get; set; }

        [JsonPropertyName("parts")]
        public List<Part> Parts { get; set; }

        [JsonPropertyName("controls")]
        public List<Control> Controls { get; set; }
    }
}
