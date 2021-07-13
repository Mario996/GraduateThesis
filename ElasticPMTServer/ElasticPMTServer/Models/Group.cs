using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ElasticPMTServer.Models.Pokusaj
{
    public class Group
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("class")]
        public string Class { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("controls")]
        public List<Control> Controls { get; set; }
    }
}
