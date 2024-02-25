using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Repository.Configuration;

using Microsoft.EntityFrameworkCore;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<MeteoStation>? MeteoStations { get; set; }
        public DbSet<MeteoPoint>? MeteoPoints { get; set; }

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeteoPoint>(entity =>
            {
                entity.HasKey(e => new { e.MeteoStationId, e.Date });
            });

            modelBuilder.ApplyConfiguration(new MeteoStationConfiguration());
        }
    }
}
