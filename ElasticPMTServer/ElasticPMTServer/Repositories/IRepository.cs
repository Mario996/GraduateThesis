using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticPMTServer.Repositories
{
    public interface IRepository
    {
        bool indexExists();
        IndexResponse create();
    }
}
