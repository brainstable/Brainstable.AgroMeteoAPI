using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Service.Contracts;

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

        public IEnumerable<MeteoStation> GetAllMeteoStations(bool trackChanges)
        {
            try
            {
                var meteoStations = repository.MeteoStation.GetAllMeteoStations(trackChanges);
                
                return meteoStations;
            }
            catch (Exception ex)
            {
                logger.LogError($"Something went wrong in the {nameof(GetAllMeteoStations)} service method {ex}");
                throw;
            }
        }
    }
}
