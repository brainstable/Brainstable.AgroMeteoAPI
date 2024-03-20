using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Exceptions;
using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Service.Contracts;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;
using Brainstable.AgroMeteoAPI.Shared.RequestParameters;

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

        public async Task<IEnumerable<MeteoStationDto>> GetAllMeteoStationsAsync(bool trackChanges)
        {
            var meteoStations = await repository.MeteoStation.GetAllMeteoStationsAsync(trackChanges);

            var meteoStationsDto = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);

            return meteoStationsDto;
        }

        public async Task<IEnumerable<MeteoStationDto>> GetAllMeteoStationsAsync(MeteoStationParameters meteoStationParameters, bool trackChanges)
        {
            var meteoStations = await repository.MeteoStation.GetAllMeteoStationsAsync(meteoStationParameters, trackChanges);

            var meteoStationsDto = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);

            return meteoStationsDto;
        }

        public async Task<MeteoStationDto> GetMeteoStationAsync(string meteoStationId, bool trackChanges)
        {
            var meteoStation = await GetMeteoStationAndCheckIfItExists(meteoStationId, trackChanges);

            var meteoStationDto = mapper.Map<MeteoStationDto>(meteoStation);
            return meteoStationDto;
        }

        public async Task<MeteoStationDto> CreateMeteoStationAsync(MeteoStationForCreationDto meteoStation)
        {
            var meteoStationEntity = mapper.Map<MeteoStation>(meteoStation);

            repository.MeteoStation.CreateMeteoStation(meteoStationEntity);
            await repository.SaveAsync();

            var meteoStationToReturn = mapper.Map<MeteoStationDto>(meteoStationEntity);

            return meteoStationToReturn;
        }

        public async Task<IEnumerable<MeteoStationDto>> GetByIdsAsync(IEnumerable<string> ids, bool trackChanges)
        {
            if (ids is null)
                throw new IdParametersBadRequestException();

            var meteoStations = await repository.MeteoStation.GetByIdsAsync(ids, trackChanges);

            if (ids.Count() != meteoStations.Count())
                throw new CollectionByIdsBadRequestException();

            var meteoStationsToReturn = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);

            return meteoStationsToReturn;
        }

        public async Task<(IEnumerable<MeteoStationDto> meteoStations, string ids)> CreateMeteoStationCollectionAsync(IEnumerable<MeteoStationForCreationDto> meteoStationCollection)
        {
            if (meteoStationCollection is null)
                throw new MeteoStationCollectionBadRequestException();

            var meteoStations = mapper.Map<IEnumerable<MeteoStation>>(meteoStationCollection);
            foreach (var meteoStation in meteoStations)
            {
                repository.MeteoStation.CreateMeteoStation(meteoStation);
            }

            await repository.SaveAsync();

            var meteoStationCollectionToReturn = mapper.Map<IEnumerable<MeteoStationDto>>(meteoStations);
            var ids = string.Join(",", meteoStationCollectionToReturn.Select(x => x.MeteoStationId));
            
            return (meteoStations: meteoStationCollectionToReturn, ids: ids);
        }

        public async Task DeleteMeteoStationAsync(string meteoStationId, bool trackChanges)
        {
            var meteoStation = await GetMeteoStationAndCheckIfItExists(meteoStationId, trackChanges);

            repository.MeteoStation.DeleteMeteoStation(meteoStation);
            await repository.SaveAsync();
        }

        public async Task UpdateMeteoStationAsync(string meteoStationId, MeteoStationForUpdateDto meteoStationUpdate, bool trackChanges)
        {
            var meteoStation = await GetMeteoStationAndCheckIfItExists(meteoStationId, trackChanges);

            mapper.Map(meteoStationUpdate, meteoStation);
            await repository.SaveAsync();
        }

        private async Task<MeteoStation> GetMeteoStationAndCheckIfItExists(string meteoStationId, bool trackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFoundException(meteoStationId);

            return meteoStation;
        }
    }
}
