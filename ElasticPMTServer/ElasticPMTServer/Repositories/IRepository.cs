using ElasticPMTServer.Models;
using Nest;

namespace ElasticPMTServer.Repositories
{
    public interface IRepository<TEntity> where TEntity : ElasticSearchType
    {
        IndexResponse create(TEntity document);
        ISearchResponse<TEntity> getAll();
        GetResponse<TEntity> getById(string id);
        UpdateResponse<TEntity> update(string id, TEntity document);
        DeleteResponse delete(string id);
        void checkIfIndexExists();
    }
}
