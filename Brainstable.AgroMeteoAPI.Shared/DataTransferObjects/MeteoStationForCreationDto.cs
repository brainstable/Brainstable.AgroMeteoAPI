namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public record MeteoStationForCreationDto :MeteoStationForManipulationDto
{
    public IEnumerable<MeteoPointForCreationDto> MeteoPoints { get; set; }
}