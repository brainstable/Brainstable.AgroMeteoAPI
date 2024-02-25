using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class MeteoPointRepository : RepositoryBase<MeteoPoint>, IMeteoPointRepository
    {
        public MeteoPointRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
