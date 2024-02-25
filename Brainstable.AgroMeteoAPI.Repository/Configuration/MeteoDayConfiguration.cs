using Brainstable.AgroMeteoAPI.Entities.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Brainstable.AgroMeteoAPI.Repository.Configuration
{
    internal class MeteoDayConfiguration : IEntityTypeConfiguration<MeteoDay>
    {
        public void Configure(EntityTypeBuilder<MeteoDay> builder)
        {
            throw new NotImplementedException();
        }
    }
}
