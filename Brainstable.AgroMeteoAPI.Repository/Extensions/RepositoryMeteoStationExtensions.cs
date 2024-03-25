using Brainstable.AgroMeteoAPI.Entities.Models;

using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Brainstable.AgroMeteoAPI.Repository.Extensions.Utility;

namespace Brainstable.AgroMeteoAPI.Repository.Extensions;

public static class RepositoryMeteoStationExtensions
{
    public static IQueryable<MeteoStation> Search(this IQueryable<MeteoStation> meteoStations, string searchTerm)
    {
        if (string.IsNullOrEmpty(searchTerm))
        {
            return meteoStations;
        }

        var lowerCaseTerm = searchTerm.Trim().ToLower();

        return meteoStations.Where(x => x.Name.ToLower().Contains(lowerCaseTerm));
    }

    public static IQueryable<MeteoStation> Paging(this IQueryable<MeteoStation> meteoStations, int pageNumber, int pageSize)
    {
        if (pageSize < 1)
        {
            return meteoStations;
        }

        return meteoStations.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }

    public static IQueryable<MeteoStation> Sort(this IQueryable<MeteoStation> meteoStations, string orderByQueryString)
    {
        if (string.IsNullOrEmpty(orderByQueryString))
        {
            return meteoStations.OrderBy(x => x.Name);
        }

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<MeteoStation>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return meteoStations.OrderBy(x => x.Name);

        return meteoStations.OrderBy(orderQuery);
    }
}