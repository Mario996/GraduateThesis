using ElasticPMTServer.Models;
using Microsoft.EntityFrameworkCore;

namespace ElasticPMTServer.Persistance
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Requirement> Requirements { get; set; }
    }
}
