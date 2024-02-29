﻿using AutoMapper;

using Brainstable.AgroMeteoAPI.Contracts;
using Brainstable.AgroMeteoAPI.Entities.Exceptions;
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
            if (meteoStation is null)
                throw new MeteoStationNotFoundException(meteoStationId);

            var meteoStationDto = mapper.Map<MeteoStationDto>(meteoStation);
            return meteoStationDto;
        }
    }
}
