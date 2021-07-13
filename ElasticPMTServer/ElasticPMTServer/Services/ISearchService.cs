using ElasticPMTServer.Models.Pokusaj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public interface ISearchService
    {
        IEnumerable<CustomControl> Autocomplete(string query, int count);
    }
}
