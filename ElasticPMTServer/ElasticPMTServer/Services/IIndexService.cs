using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Services
{
    public interface IIndexService
    {
        BulkResponse populateIndex();
        bool indexExists();
    }
}
