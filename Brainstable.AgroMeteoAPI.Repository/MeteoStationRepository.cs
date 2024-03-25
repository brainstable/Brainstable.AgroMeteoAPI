using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Repository.Extensions;
using Brainstable.AgroMeteoAPI.Shared.RequestParameters;
using Microsoft.EntityFrameworkCore;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public class MeteoStationRepository : RepositoryBase<MeteoStation>, IMeteoStationRepository
    {
        public MeteoStationRepository(RepositoryContext repositoryContext) 
            : base(repositoryContext)
        {
        }

        //public async Task<IEnumerable<MeteoStation>> GetAllMeteoStationsAsync(bool trackChanges) =>
        //    await FindAll(trackChanges)
        //        .OrderBy(x => x.Name)
        //        .ToListAsync();

        public async Task<PagedList<MeteoStation>> GetAllMeteoStationsAsync(MeteoStationParameters meteoStationParameters, bool trackChanges)
        {
            var meteoStations = await FindAll(trackChanges)
                .Search(meteoStationParameters.SearchTerm)
                .Sort(meteoStationParameters.OrderBy)
                .ToListAsync();

            return PagedList<MeteoStation>.ToPagedList(meteoStations, meteoStationParameters.PageNumber, meteoStationParameters.PageSize);
        }

        public async Task<MeteoStation> GetMeteoStationAsync(string meteoStationId, bool trackChanges) =>
            await FindByCondition(x => x.MeteoStationId.Equals(meteoStationId), trackChanges)
                .SingleOrDefaultAsync();

        public void CreateMeteoStation(MeteoStation meteoStation) => Create(meteoStation);
        public async Task<IEnumerable<MeteoStation>> GetByIdsAsync(IEnumerable<string> meteoStationIds, bool trackChanges)
        {
            return await FindByCondition(x => meteoStationIds.Contains(x.MeteoStationId), trackChanges).ToListAsync();
        }

        public void DeleteMeteoStation(MeteoStation meteoStation)
        {
            Delete(meteoStation);
        }
    }
}
