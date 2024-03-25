namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects
{
    public record MeteoStationDto(string MeteoStationId, string Name, double Latitude, double Longitude, string NameRus, string NameEng, string Country);
}
