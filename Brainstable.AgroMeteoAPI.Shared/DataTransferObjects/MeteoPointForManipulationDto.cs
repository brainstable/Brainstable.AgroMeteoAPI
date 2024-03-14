using System.ComponentModel.DataAnnotations;

namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public abstract record MeteoPointForManipulationDto
{

    [Range(-100.0, 100.0, ErrorMessage = "")]
    public double? Temperature { get; set; }

    [Range(-100.0, 100.0, ErrorMessage = "")]
    public double? MinTemperature { get; set; }

    [Range(-100.0, 100.0, ErrorMessage = "")]
    public double? MaxTemperature { get; set; }

    [Range(0.0, 10000.0, ErrorMessage = "")]
    public double? Rainfall { get; set; }

    [Range(0.0, 10000.0, ErrorMessage = "")]
    public double? SnowHight { get; set; }

    [Range(0.0, 100.0, ErrorMessage = "")]
    public double? Humidity { get; set; }
}