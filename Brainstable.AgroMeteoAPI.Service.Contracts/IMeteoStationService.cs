using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoStationService
    {
        IEnumerable<MeteoStationDto> GetAllMeteoStations(bool trackChanges);
        MeteoStationDto GetMeteoStation(string meteoStationId, bool trackChanges);
        MeteoStationDto CreateMeteoStation(MeteoStationForCreationDto meteoStation);
        IEnumerable<MeteoStationDto> GetByIds(IEnumerable<string> ids, bool trackChanges);
        (IEnumerable<MeteoStationDto> meteoStations, string ids) CreateMeteoStationCollection(
            IEnumerable<MeteoStationForCreationDto> meteoStationCollection);
        void DeleteMeteoStation(string meteoStationId, bool trackChanges);
    }
}
