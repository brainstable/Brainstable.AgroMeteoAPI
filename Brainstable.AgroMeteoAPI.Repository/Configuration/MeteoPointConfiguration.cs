using Brainstable.AgroMeteoAPI.Entities.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brainstable.AgroMeteoAPI.Repository.Configuration
{
    internal class MeteoPointConfiguration : IEntityTypeConfiguration<MeteoPoint>
    {
        readonly string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "meteo_stations.csv");

        public void Configure(EntityTypeBuilder<MeteoPoint> builder)
        {
            throw new NotImplementedException();
        }
    }
}
