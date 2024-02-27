using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service
{
    internal sealed class MeteoStationService : IMeteoStationService
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;

        public MeteoStationService(IRepositoryManager repository, ILoggerManager logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public IEnumerable<MeteoStationDto> GetAllMeteoStations(bool trackChanges)
        {
            try
            {
                var meteoStations = repository.MeteoStation.GetAllMeteoStations(trackChanges);

                var meteoStationsDto = meteoStations.Select(x =>
                    new MeteoStationDto(x.MeteoStationId, x.Name,
                        x.Latitude.HasValue ? x.Latitude.Value : 0,
                        x.Longitude.HasValue ? x.Longitude.Value : 0));
                
                return meteoStationsDto;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetAllMeteoStations)} service method {ex}");
                throw;
            }
        }
    }
}
