using ElasticPMTServer.Models.Pokusaj;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public class SearchService : ISearchService
    {
        protected readonly ElasticClient _elasticJsonClient;
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
                //var result = _elasticJsonClient.Search<CustomControl>(x => x
                //                    .Suggest(su => su
                //                        .Completion("custom-control-suggestions", c => c
                //                            .Field(f => f.Suggest)
                //                            .Prefix(query)
                //                            .Fuzzy(f => f
                //                        .Fuzziness(Fuzziness.Auto))
                //                        .Size(count))));
                var result = _elasticJsonClient.Search<CustomControl>(x => x
                                    .Query(q => q
                                    .Match(m => m.Field(
                                        f => f.PartProse)
                                    .Query(query)))
                                    .Size(count));

                return result.Documents;
                //return result.Suggest["custom-control-suggestions"].SelectMany(x => x.Options)
                //.Select(y => y.Source);
            }
            else
            {
                return Enumerable.Empty<CustomControl>();
            }

        }
    }
}
