using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Service.Contracts;

namespace Brainstable.AgroMeteoAPI.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMeteoStationService> meteoStationService;
        private readonly Lazy<IMeteoPointService> meteoPointService;

        public ServiceManager(IRepositoryManager repositoryManager, 
                              ILoggerManager logger,
                              IMapper mapper)
        {
            meteoStationService = new Lazy<IMeteoStationService>(() => new MeteoStationService(repositoryManager, logger, mapper));
            meteoPointService = new Lazy<IMeteoPointService>(() => new MeteoPointService(repositoryManager, logger, mapper));
        }

        public IMeteoStationService MeteoStationService => meteoStationService.Value;
        public IMeteoPointService MeteoPointService => meteoPointService.Value;
    }
}
