using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoPointService
    {
        Task<IEnumerable<MeteoPointDto>> GetAllDaysMeteoPointsAsync(string meteoStationId, bool trackChanges);
        Task<Dictionary<DateOnly, double?>> GetAllDaysTemperatureAsync(string meteoStationId, bool trackChanges);
        Task<MeteoPointDto> GetMeteoPointAsync(string meteoStationId, DateOnly date, bool trackChanges);
        Task<IEnumerable<MeteoPointDto>> GetDaysMeteoPointsAsync(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges);
        Task<MeteoPointDto> CreateMeteoPointForMeteoStationAsync(string meteoStationId, MeteoPointForCreationDto meteoPointForCreation, bool trackChanges);
        Task DeleteMeteoPointForMeteoStationAsync(string meteoStationId, DateOnly date, bool trackChanges);
        Task UpdateMeteoPointForMeteoStationAsync(string meteoStationId, DateOnly date, MeteoPointForUpdateDto meteoPointForUpdate, bool stationTrackChanges, bool pointTrackChanges); 
        Task<(MeteoPointForUpdateDto meteoPointToPatch, MeteoPoint meteoPoint)> GetMeteoPointForPatchAsync(string meteoStationId, DateOnly date, bool stationTrackChanges, bool pointTrackChanges);
        Task SaveChangesForPatchAsync(MeteoPointForUpdateDto meteoPointToPatch, MeteoPoint meteoPoint);
    }
}
