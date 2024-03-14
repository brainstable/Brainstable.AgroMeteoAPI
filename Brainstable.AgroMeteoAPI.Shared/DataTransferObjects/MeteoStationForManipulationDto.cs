using System.ComponentModel.DataAnnotations;

namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public abstract record MeteoStationForManipulationDto
{
    [Required(ErrorMessage = "MeteoStationId is a required field")]
    [MaxLength(6, ErrorMessage = "Maximum length for the MeteoStationId is 6 characters")]
    public string MeteoStationId { get; set; }

    [Required(ErrorMessage = "Name is a required field")]
    [MaxLength(100, ErrorMessage = "Maximum length for the Name is 100 characters")]
    public string Name { get; set; }
    [Range(-180, 180, ErrorMessage = "")]
    public double? Latitude { get; set; }
    [Range(-180, 180, ErrorMessage = "")]
    public double? Longitude { get; set; }

    [Range(-400, 8848, ErrorMessage = "")]
    public double? Altitude { get; set; }

    [MaxLength(100, ErrorMessage = "Maximum length for the NameRus is 100 characters")]
    public string? NameRus { get; set; }

    [MaxLength(100, ErrorMessage = "Maximum length for the NameEng is 100 characters")]
    public string? NameEng { get; set; }

    [MaxLength(50, ErrorMessage = "Maximum length for the Country is 50 characters")]
    public string? Country { get; set; }
    public IEnumerable<MeteoPointForCreationDto> MeteoPoints { get; set; }
}