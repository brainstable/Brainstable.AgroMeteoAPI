﻿namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects;

public class MeteoPointForUpdateDto
{
    public double? Temperature { get; set; }
    public double? MinTemperature { get; set; }
    public double? MaxTemperature { get; set; }
    public double? Rainfall { get; set; }
    public double? SnowHight { get; set; }
    public double? Humidity { get; set; }
}