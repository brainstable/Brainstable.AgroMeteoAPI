using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class MeteoPointRepository : RepositoryBase<MeteoPoint>, IMeteoPointRepository
    {
        public MeteoPointRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<IEnumerable<MeteoPoint>> GetAllDaysMeteoPointsAsync(string meteoStationId, bool trackChanges)
        {
            return await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges).ToListAsync();
        }

        public async Task<Dictionary<DateOnly, double?>> GetAllDaysTemperatureAsync(string meteoStationId, bool trackChanges)
        {
            var meteoPoints = await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges)
                .Select(x => new
                {
                    Date = x.Date,
                    Temperature = x.Temperature
                })
                .ToListAsync();

            Dictionary<DateOnly, double?> allDaysTemperature = new Dictionary<DateOnly, double?>();
            foreach (var tempMeteoPoint in meteoPoints)
            {
                allDaysTemperature[tempMeteoPoint.Date] = tempMeteoPoint.Temperature;
            }

            return allDaysTemperature;
        }

        public async Task<MeteoPoint> GetMeteoPointAsync(string meteoStationId, DateOnly date, bool trackChanges)
        {
            return await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId) && x.Date == date, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<MeteoPoint>> GetDaysMeteoPointsAsync(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges)
        {
            return await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId) && x.Date >= startDate && x.Date <= endDate, trackChanges).ToListAsync();
        }

        public void CreateMeteoPointForMeteoStation(string meteoStation, MeteoPoint meteoPoint)
        {
            meteoPoint.MeteoStationId = meteoStation;
            Create(meteoPoint);
        }

        public void DeleteMeteoPoint(MeteoPoint meteoPoint)
        {
            Delete(meteoPoint);
        }
    }
}
