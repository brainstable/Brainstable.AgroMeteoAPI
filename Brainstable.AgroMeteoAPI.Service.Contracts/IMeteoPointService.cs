using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoPointService
    {
        IEnumerable<MeteoPointDto> GetAllDaysMeteoPoints(string meteoStationId, bool trackChanges);
        Dictionary<DateOnly, double?> GetAllDaysTemperature(string meteoStationId, bool trackChanges);
    }
}
