using Brainstable.AgroMeteoAPI.Entities.Models;

namespace Brainstable.AgroMeteoAPI.Repository.Extensions
{
    public static class RepositoryMeteoPointExtensions
    {
        public static IQueryable<MeteoPoint> FilterBetweenDates(this IQueryable<MeteoPoint> meteoPoints, DateOnly minDate, DateOnly maxDate)
        {
            return meteoPoints.Where(x => x.Date >= minDate && x.Date <= maxDate);
        }

        public static IQueryable<MeteoPoint> Paging(this IQueryable<MeteoPoint> meteoPoints, int pageNumber, int pageSize)
        {
            if (pageSize < 1)
            {
                return meteoPoints;
            }
            
            return meteoPoints.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }
    }
}
