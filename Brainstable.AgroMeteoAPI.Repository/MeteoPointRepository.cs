using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Shared.RequestParameters;
using Microsoft.EntityFrameworkCore;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class MeteoPointRepository : RepositoryBase<MeteoPoint>, IMeteoPointRepository
    {
        public MeteoPointRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public async Task<PagedList<MeteoPoint>> GetAllDaysMeteoPointsAsync(string meteoStationId, MeteoPointParameters meteoPointParameters, bool trackChanges)
        {
            bool del(MeteoPoint x)
            {
                return 
            }

            
            var meteoPoints = await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId) 
                                                         && (x.Date >= meteoPointParameters.MinDate && x.Date <= meteoPointParameters.MaxDate), trackChanges)
                .OrderBy(x => x.Date)
                .Skip((meteoPointParameters.PageNumber - 1) * meteoPointParameters.PageSize)
                .Take(meteoPointParameters.PageSize)
                .ToListAsync();

            var count = await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId) 
                                                   && (x.Date >= meteoPointParameters.MinDate && x.Date <= meteoPointParameters.MaxDate), trackChanges)
                .CountAsync();

            return new PagedList<MeteoPoint>(meteoPoints, count, meteoPointParameters.PageNumber, meteoPointParameters.PageSize);
        }

        public async Task<Dictionary<DateOnly, double?>> GetAllDaysTemperatureAsync(string meteoStationId, MeteoPointParameters meteoPointParameters, bool trackChanges)
        {
            var meteoPoints = await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges)
                .Select(x => new
                {
                    Date = x.Date,
                    Temperature = x.Temperature
                })
                .OrderBy(x => x.Date)
                .Skip((meteoPointParameters.PageNumber - 1) * meteoPointParameters.PageSize)
                .Take(meteoPointParameters.PageSize)
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
