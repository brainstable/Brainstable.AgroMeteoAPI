using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class MeteoStationRepository : RepositoryBase<MeteoStation>, IMeteoStationRepository
    {
        public MeteoStationRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        public IEnumerable<MeteoStation> GetAllMeteoStations(bool trackChanges) =>
            FindAll(trackChanges)
                .OrderBy(x => x.Name)
                .ToList();
    }
}
