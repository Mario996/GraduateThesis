using ElasticPMTServer.Models;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public class SearchService : ISearchService
    {
        private readonly ElasticClient _elasticJsonClient;
        private readonly ConnectionSettings _settingsJson;

        public SearchService()
        {
            _settingsJson = new ConnectionSettings()
                               .DefaultMappingFor<CustomControl>(m => m
                               .DisableIdInference()
                               .IndexName("jsonindex")
                               );

            _elasticJsonClient = new ElasticClient(_settingsJson);
        }
        public IEnumerable<CustomControl> Autocomplete(string query, int count)
        {
            if (query != null)
            {
                var result = _elasticJsonClient.Search<CustomControl>(x => x
                                    .Query(q => q
                                    .Match(m => m.Field(
                                        f => f.PartProse)
                                    .Query(query)))
                                    .Size(count));

                return result.Documents;
            }
            return Enumerable.Empty<CustomControl>();
        }
    }
}
