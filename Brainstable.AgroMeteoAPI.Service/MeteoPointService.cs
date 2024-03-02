using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI.Service
{
    internal sealed class MeteoPointService : IMeteoPointService
    {
        private readonly IRepositoryManager repository;
        private readonly ILoggerManager logger;
        private readonly IMapper mapper;

        public MeteoPointService(IRepositoryManager repository, ILoggerManager logger, AutoMapper.IMapper mapper)
        {
            this.repository = repository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public IEnumerable<MeteoPointDto> GetAllDaysMeteoPoints(string meteoStationId, bool trackChanges)
        {
            var meteoPoints = repository.MeteoPoint.GetAllDaysMeteoPoints(meteoStationId, trackChanges);
            // exception

            var meteoPointsDto = mapper.Map<IEnumerable<MeteoPointDto>>(meteoPoints);

            return meteoPointsDto;
        }

        public Dictionary<DateOnly, double?> GetAllDaysTemperature(string meteoStationId, bool trackChanges)
        {
            return repository.MeteoPoint.GetAllDaysTemperature(meteoStationId, trackChanges);
        }

        public MeteoPointDto GetMeteoPoint(string meteoStationId, DateOnly date, bool trackChanges)
        {
            var meteoPoint = repository.MeteoPoint.GetMeteoPoint(meteoStationId, date, trackChanges);
            // exception

            var meteoPointDto = mapper.Map<MeteoPointDto>(meteoPoint);

            return meteoPointDto;
        }

        public IEnumerable<MeteoPointDto> GetDaysMeteoPoints(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges)
        {
            var meteoPoints = repository.MeteoPoint.GetDaysMeteoPoints(meteoStationId, startDate, endDate, trackChanges);
            // exception

            var meteoPointsDto = mapper.Map<IEnumerable<MeteoPointDto>>(meteoPoints);

            return meteoPointsDto;
        }
    }
}
