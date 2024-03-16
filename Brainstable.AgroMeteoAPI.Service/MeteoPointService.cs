using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Exceptions;
using Brainstable.AgroMeteoAPI.Entities.Models;
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

        public async Task<IEnumerable<MeteoPointDto>> GetAllDaysMeteoPointsAsync(string meteoStationId, bool trackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoPoints = await repository.MeteoPoint.GetAllDaysMeteoPointsAsync(meteoStationId, trackChanges);

            var meteoPointsDto = mapper.Map<IEnumerable<MeteoPointDto>>(meteoPoints);

            return meteoPointsDto;
        }

        public async Task<Dictionary<DateOnly, double?>> GetAllDaysTemperatureAsync(string meteoStationId, bool trackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            return await repository.MeteoPoint.GetAllDaysTemperatureAsync(meteoStationId, trackChanges);
        }

        public async Task<MeteoPointDto> GetMeteoPointAsync(string meteoStationId, DateOnly date, bool trackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoPoint = await repository.MeteoPoint.GetMeteoPointAsync(meteoStationId, date, trackChanges);

            var meteoPointDto = mapper.Map<MeteoPointDto>(meteoPoint);

            return meteoPointDto;
        }

        public async Task<IEnumerable<MeteoPointDto>> GetDaysMeteoPointsAsync(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoPoints = await repository.MeteoPoint.GetDaysMeteoPointsAsync(meteoStationId, startDate, endDate, trackChanges);

            var meteoPointsDto = mapper.Map<IEnumerable<MeteoPointDto>>(meteoPoints);

            return meteoPointsDto;
        }

        public async Task<MeteoPointDto> CreateMeteoPointForMeteoStationAsync(string meteoStationId, MeteoPointForCreationDto meteoPointForCreation,
            bool trackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, trackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoPoint = mapper.Map<MeteoPoint>(meteoPointForCreation);

            repository.MeteoPoint.CreateMeteoPointForMeteoStation(meteoStationId, meteoPoint);
            await repository.SaveAsync();

            var meteoPointToReturn = mapper.Map<MeteoPointDto>(meteoPoint);

            return meteoPointToReturn;
        }

        public async Task DeleteMeteoPointForMeteoStationAsync(string meteoStationId, DateOnly date, bool trackChanges)
        {
            var meteoPoint = await repository.MeteoPoint.GetMeteoPointAsync(meteoStationId, date, trackChanges);
            if (meteoPoint is null)
                throw new MeteoPointNotFound(meteoStationId, date);

            repository.MeteoPoint.DeleteMeteoPoint(meteoPoint);
            await repository.SaveAsync();
        }

        public async Task UpdateMeteoPointForMeteoStationAsync(string meteoStationId, DateOnly date, MeteoPointForUpdateDto meteoPointForUpdate,
            bool stationTrackChanges, bool pointTrackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, stationTrackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoPoint = await repository.MeteoPoint.GetMeteoPointAsync(meteoStationId, date, pointTrackChanges);
            if (meteoPoint is null)
                throw new MeteoPointNotFound(meteoStationId, date);

            mapper.Map(meteoPointForUpdate, meteoPoint);
            await repository.SaveAsync();
        }

        public async Task<(MeteoPointForUpdateDto meteoPointToPatch, MeteoPoint meteoPoint)> GetMeteoPointForPatchAsync(string meteoStationId,
            DateOnly date, bool stationTrackChanges, bool pointTrackChanges)
        {
            var meteoStation = await repository.MeteoStation.GetMeteoStationAsync(meteoStationId, stationTrackChanges);
            if (meteoStation is null)
                throw new MeteoStationNotFound(meteoStationId);

            var meteoPointEntity = await repository.MeteoPoint.GetMeteoPointAsync(meteoStationId, date, pointTrackChanges);
            if (meteoPointEntity is null)
                throw new MeteoPointNotFound(meteoStationId, date);

            var meteoPointToPatch = mapper.Map<MeteoPointForUpdateDto>(meteoPointEntity);

            return (meteoPointToPatch, meteoPointEntity);
        }

        public async Task SaveChangesForPatchAsync(MeteoPointForUpdateDto meteoPointToPatch, MeteoPoint meteoPoint)
        {
            mapper.Map(meteoPointToPatch, meteoPoint);
            await repository.SaveAsync();
        }
    }
}
