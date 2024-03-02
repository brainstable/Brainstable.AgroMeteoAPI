using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class MeteoPointRepository : RepositoryBase<MeteoPoint>, IMeteoPointRepository
    {
        public MeteoPointRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public IEnumerable<MeteoPoint> GetAllDaysMeteoPoints(string meteoStationId, bool trackChanges)
        {
            return FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges);
        }

        public Dictionary<DateOnly, double?> GetAllDaysTemperature(string meteoStationId, bool trackChanges)
        {
            var meteoPoints = FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges)
                .Select(x => new
                {
                    Date = x.Date,
                    Temperature = x.Temperature
                })
                .ToList();

            Dictionary<DateOnly, double?> allDaysTemperature = new Dictionary<DateOnly, double?>();
            foreach (var tempMeteoPoint in meteoPoints)
            {
                allDaysTemperature[tempMeteoPoint.Date] = tempMeteoPoint.Temperature;
            }

            return allDaysTemperature;
        }

        public MeteoPoint GetMeteoPoint(string meteoStationId, DateOnly date, bool trackChanges)
        {
            return FindByCondition(x => x.MeteoStationId.Equals(meteoStationId) && x.Date == date, trackChanges).SingleOrDefault();
        }

        public IEnumerable<MeteoPoint> GetDaysMeteoPoints(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges)
        {
            return FindByCondition(
                x => x.MeteoStationId.Equals(meteoStationId) && x.Date >= startDate && x.Date <= endDate, trackChanges);
        }
    }
}
