using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoStationService
    {
        IEnumerable<MeteoStation> GetAllMeteoStations(bool trackChanges);
    }
}
