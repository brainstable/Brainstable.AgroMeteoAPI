using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoPointRepository
    {
        Task<IEnumerable<MeteoPoint>> GetAllDaysMeteoPointsAsync(string meteoStationId, bool trackChanges);
        Task<Dictionary<DateOnly, double?>> GetAllDaysTemperatureAsync(string meteoStationId, bool trackChanges);
        Task<MeteoPoint> GetMeteoPointAsync(string meteoStationId, DateOnly date, bool trackChanges);
        Task<IEnumerable<MeteoPoint>> GetDaysMeteoPointsAsync(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges);
        void CreateMeteoPointForMeteoStation(string meteoStation, MeteoPoint meteoPoint);
        void DeleteMeteoPoint(MeteoPoint meteoPoint);
    }
}
