using System.Reflection;
using System.Text;
using Brainstable.AgroMeteoAPI.Entities.Models;
using System.Linq.Dynamic.Core;
using Brainstable.AgroMeteoAPI.Repository.Extensions.Utility;

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

        public static IQueryable<MeteoPoint> Sort(this IQueryable<MeteoPoint> meteoPoints, string orderByQueryString)
        {
            if (string.IsNullOrEmpty(orderByQueryString))
            {
                return meteoPoints.OrderBy(x => x.Date);
            }

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<MeteoPoint>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return meteoPoints.OrderBy(x => x.Date);

            return meteoPoints.OrderBy(orderQuery);
        }
    }
}
