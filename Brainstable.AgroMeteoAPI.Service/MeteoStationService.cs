using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service
{
    internal sealed class MeteoStationService : IMeteoStationService
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public MeteoStationService(IRepositoryManager repository, ILoggerManager logger, AutoMapper.IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IEnumerable<MeteoStationDto> GetAllMeteoStations(bool trackChanges)
        {
            try
            {
                var meteoStations = repository.MeteoStation.GetAllMeteoStations(trackChanges);

                var meteoStationsDto = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);
                
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
