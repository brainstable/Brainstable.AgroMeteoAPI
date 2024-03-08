using System.ComponentModel.DataAnnotations.Schema;

namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public class MeteoStationForCreationDto
{
    public string MeteoStationId { get; set; }
    public string Name { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Altitude { get; set; }
    public string? NameRus { get; set; }
    public string? NameEng { get; set; }
    public string? Country { get; set; }
    public IEnumerable<MeteoPointForCreationDto> MeteoPoints { get; set; }
}