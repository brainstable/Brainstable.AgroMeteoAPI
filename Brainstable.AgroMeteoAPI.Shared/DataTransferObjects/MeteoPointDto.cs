namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public record MeteoPointDto(
    DateOnly date,
    double? Temperature, 
    double? MinTemperature, 
    double? MaxTemperature, 
    double? Rainfall, 
    double? SnowHight, 
    double? Humidity);