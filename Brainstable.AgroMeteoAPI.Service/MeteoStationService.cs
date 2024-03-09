using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Exceptions;
using Brainstable.AgroMeteoAPI.Entities.Models;
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
            var meteoStations = repository.MeteoStation.GetAllMeteoStations(trackChanges);

            var meteoStationsDto = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);

            return meteoStationsDto;
        }

        public MeteoStationDto GetMeteoStation(string meteoStationId, bool trackChanges)
        {
            var meteoStation = repository.MeteoStation.GetMeteoStation(meteoStationId, trackChanges);
            if (meteoStation == null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoStationDto = mapper.Map<MeteoStationDto>(meteoStation);
            return meteoStationDto;
        }

        public MeteoStationDto CreateMeteoStation(MeteoStationForCreationDto meteoStation)
        {
            var meteoStationEntity = mapper.Map<MeteoStation>(meteoStation);

            repository.MeteoStation.CreateMeteoStation(meteoStationEntity);
            repository.Save();

            var meteoStationToReturn = mapper.Map<MeteoStationDto>(meteoStationEntity);

            return meteoStationToReturn;
        }

        public IEnumerable<MeteoStationDto> GetByIds(IEnumerable<string> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequest();

            var meteoStations = repository.MeteoStation.GetByIds(ids, trackChanges);

            if (ids.Count() != meteoStations.Count())
                throw new CollectionByIdsBadRequest();

            var meteoStationsToReturn = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);

            return meteoStationsToReturn;
        }

        public (IEnumerable<MeteoStationDto> meteoStations, string ids) CreateMeteoStationCollection(IEnumerable<MeteoStationForCreationDto> meteoStationCollection)
        {
            if (meteoStationCollection is null)
                throw new MeteoStationCollectionBadRequest();

            var meteoStations = mapper.Map<IEnumerable<MeteoStation>>(meteoStationCollection);
            foreach (var meteoStation in meteoStations)
            {
                repository.MeteoStation.CreateMeteoStation(meteoStation);
            }

            repository.Save();

            var meteoStationCollectionToReturn = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);
            var ids = string.Join(",", meteoStationCollectionToReturn.Select(x => x.MeteoStationId));
            
            return (meteoStations: meteoStationCollectionToReturn, ids: ids);
        }

        public void DeleteMeteoStation(string meteoStationId, bool trackChanges)
        {
            var meteoStation = repository.MeteoStation.GetMeteoStation(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            repository.MeteoStation.DeleteMeteoStation(meteoStation);
            repository.Save();
        }
    }
}
