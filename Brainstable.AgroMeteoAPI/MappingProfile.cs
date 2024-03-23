using AutoMapper;

using Brainstable.AgroMeteoAPI.Entities.Models;
using Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

namespace Brainstable.AgroMeteoAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<MeteoStation, MeteoStationDto>();
            CreateMap<MeteoStationForCreationDto, MeteoStation>();
            CreateMap<MeteoStationForUpdateDto, MeteoStation>();

            CreateMap<MeteoPoint, MeteoPointDto>();
            CreateMap<MeteoPointForCreationDto, MeteoPoint>();
            CreateMap<MeteoPointForUpdateDto, MeteoPoint>().ReverseMap();

            CreateMap<MeteoPoint, TemperatureParameter>();
        }
    }
}
