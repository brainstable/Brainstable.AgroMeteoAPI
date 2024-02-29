using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;

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
    }
}
