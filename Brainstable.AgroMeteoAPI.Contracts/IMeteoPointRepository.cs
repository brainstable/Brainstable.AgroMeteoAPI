using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoPointRepository
    {
        IEnumerable<MeteoPoint> GetAllDaysMeteoPoints(string meteoStationId, bool trackChanges);
        Dictionary<DateOnly, double?> GetAllDaysTemperature(string meteoStationId, bool trackChanges);
        MeteoPoint GetMeteoPoint(string meteoStationId, DateOnly date, bool trackChanges);
        IEnumerable<MeteoPoint> GetDaysMeteoPoints(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges);
        void CreateMeteoPointForMeteoStation(string meteoStation, MeteoPoint meteoPoint);
        void DeleteMeteoPoint(MeteoPoint meteoPoint);
    }
}
