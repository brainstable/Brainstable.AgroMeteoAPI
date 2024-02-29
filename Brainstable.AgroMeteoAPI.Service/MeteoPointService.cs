﻿using AutoMapper;

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
    }
}
