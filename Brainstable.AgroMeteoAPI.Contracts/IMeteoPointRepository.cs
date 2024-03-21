﻿using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Shared.RequestParameters;

namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IMeteoPointRepository
    {
        Task<PagedList<MeteoPoint>> GetAllDaysMeteoPointsAsync(string meteoStationId, MeteoPointParameters meteoPointParameters, bool trackChanges);
        Task<Dictionary<DateOnly, double?>> GetAllDaysTemperatureAsync(string meteoStationId, MeteoPointParameters meteoPointParameters, bool trackChanges);
        Task<MeteoPoint> GetMeteoPointAsync(string meteoStationId, DateOnly date, bool trackChanges);
        Task<IEnumerable<MeteoPoint>> GetDaysMeteoPointsAsync(string meteoStationId, DateOnly startDate, DateOnly endDate, bool trackChanges);
        void CreateMeteoPointForMeteoStation(string meteoStation, MeteoPoint meteoPoint);
        void DeleteMeteoPoint(MeteoPoint meteoPoint);
    }
}
