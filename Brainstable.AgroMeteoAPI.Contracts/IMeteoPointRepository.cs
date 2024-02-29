using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoPointRepository
    {
        IEnumerable<MeteoPoint> GetAllDaysMeteoPoints(string meteoStationId, bool trackChanges);
        Dictionary<DateOnly, double?> GetAllDaysTemperature(string meteoStationId, bool trackChanges);
    }
}
