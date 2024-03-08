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

        public MeteoStation GetMeteoStation(string meteoStationId, bool trackChanges) =>
            FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges)
                .SingleOrDefault();

        public void CreateMeteoStation(MeteoStation meteoStation) => Create(meteoStation);
        public IEnumerable<MeteoStation> GetByIds(IEnumerable<string> meteoStationIds, bool trackChanges)
        {
            return FindByCondition(x => meteoStationIds.Contains(x.MeteoStationId), trackChanges);
        }
    }
}
