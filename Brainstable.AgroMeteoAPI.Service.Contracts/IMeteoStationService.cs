using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IMeteoStationService
    {
        IEnumerable<MeteoStationDto> GetAllMeteoStations(bool trackChanges);
    }
}
