namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public record MeteoStationForUpdateDto : MeteoPointForManipulationDto
{
    public IEnumerable<MeteoPointForCreationDto> MeteoPoints { get; set; }
}