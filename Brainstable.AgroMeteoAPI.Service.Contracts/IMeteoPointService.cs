using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoPointService
    {
        IEnumerable<MeteoPointDto> GetAllDaysMeteoPoints(string meteoStationId, bool trackChanges);
        Dictionary<DateOnly, double?> GetAllDaysTemperature(string meteoStationId, bool trackChanges);
        MeteoPointDto GetMeteoPoint(string meteoStationId, DateOnly date, bool trackChanges);
        IEnumerable<MeteoPointDto> GetDaysMeteoPoints(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges);

        MeteoPointDto CreateMeteoPointForMeteoStation(string meteoStationId,
            MeteoPointForCreationDto meteoPointForCreation, bool trackChanges);
    }
}
