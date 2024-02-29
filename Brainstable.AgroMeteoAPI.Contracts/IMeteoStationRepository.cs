using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoStationRepository
    {
        IEnumerable<MeteoStation> GetAllMeteoStations(bool trackChanges);
        MeteoStation GetMeteoStation(string meteoStationId, bool trackChanges);
    }
}
