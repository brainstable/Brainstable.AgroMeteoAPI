using Brainstable.AgroMeteoAPI.Entities.Models;

using Microsoft.EntityFrameworkCore;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class RepositoryContext : DbContext
    {
        public DbSet<MeteoStation>? MeteoStations { get; set; }
        public DbSet<MeteoDay>? MeteoDays { get; set; }

        public RepositoryContext(DbContextOptions options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeteoDay>(entity =>
            {
                entity.HasKey(e => new { e.MeteoStationId, e.Date });
            });
        }
    }
}
