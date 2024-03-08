using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoStationRepository
    {
        IEnumerable<MeteoStation> GetAllMeteoStations(bool trackChanges);
        MeteoStation GetMeteoStation(string meteoStationId, bool trackChanges);
        void CreateMeteoStation(MeteoStation meteoStation);
        IEnumerable<MeteoStation> GetByIds(IEnumerable<string> meteoStationIds, bool trackChanges);
    }
}
