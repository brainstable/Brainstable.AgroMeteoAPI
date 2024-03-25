using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IMeteoStationService> meteoStationService;
        private readonly Lazy<IMeteoPointService> meteoPointService;

        public ServiceManager(IRepositoryManager repositoryManager, 
                              ILoggerManager logger,
                              IMapper mapper,
                              IDataShaper<MeteoStationDto> meteoStationDtoDataShaper,
                              IDataShaper<MeteoPointDto> meteoPointDataShaper)
        {
            meteoStationService = new Lazy<IMeteoStationService>(() => new MeteoStationService(repositoryManager, logger, mapper, meteoStationDtoDataShaper));
            meteoPointService = new Lazy<IMeteoPointService>(() => new MeteoPointService(repositoryManager, logger, mapper, meteoPointDataShaper));
        }

        public IMeteoStationService MeteoStationService => meteoStationService.Value;
        public IMeteoPointService MeteoPointService => meteoPointService.Value;
    }
}
