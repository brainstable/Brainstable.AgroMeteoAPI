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
            CreateMap<MeteoPoint, MeteoPointDto>();
            CreateMap<MeteoStationForCreationDto, MeteoStation>();
            CreateMap<MeteoPointForCreationDto, MeteoPoint>();
            CreateMap<MeteoPointForUpdateDto, MeteoPoint>();
        }
    }
}
