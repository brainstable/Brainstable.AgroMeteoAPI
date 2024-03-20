using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Shared.RequestParameters;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoStationRepository
    {
        Task<IEnumerable<MeteoStation>> GetAllMeteoStationsAsync(bool trackChanges);
        Task<IEnumerable<MeteoStation>> GetAllMeteoStationsAsync(MeteoStationParameters meteoStationParameters, bool trackChanges);
        Task<MeteoStation> GetMeteoStationAsync(string meteoStationId, bool trackChanges);
        void CreateMeteoStation(MeteoStation meteoStation);
        Task<IEnumerable<MeteoStation>> GetByIdsAsync(IEnumerable<string> meteoStationIds, bool trackChanges);
        void DeleteMeteoStation(MeteoStation meteoStation);
    }
}
