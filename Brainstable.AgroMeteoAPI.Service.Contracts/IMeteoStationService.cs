using System.Dynamic;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;
using Brainstable.AgroMeteoAPI.Shared.RequestParameters;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoStationService
    {
        //Task<IEnumerable<MeteoStationDto>> GetAllMeteoStationsAsync(bool trackChanges);
        Task<(IEnumerable<ExpandoObject> meteoStationDtos, MetaData metaData)> GetAllMeteoStationsAsync(MeteoStationParameters meteoStationParameters, bool trackChanges);
        Task<IEnumerable<MeteoStationDto>> GetAllMeteoStationsAsync(bool trackChanges);
        Task<MeteoStationDto> GetMeteoStationAsync(string meteoStationId, bool trackChanges);
        Task<MeteoStationDto> CreateMeteoStationAsync(MeteoStationForCreationDto meteoStation);
        Task<IEnumerable<MeteoStationDto>> GetByIdsAsync(IEnumerable<string> ids, bool trackChanges);
        Task<(IEnumerable<MeteoStationDto> meteoStations, string ids)> CreateMeteoStationCollectionAsync(IEnumerable<MeteoStationForCreationDto> meteoStationCollection);
        Task DeleteMeteoStationAsync(string meteoStationId, bool trackChanges);
        Task UpdateMeteoStationAsync(string meteoStationId, MeteoStationForUpdateDto meteoStationUpdate, bool trackChanges);
    }
}
