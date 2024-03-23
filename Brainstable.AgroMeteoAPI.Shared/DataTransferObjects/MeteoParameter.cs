namespace Brainstable.AgroMeteoAPI.Shared.DataTransferObjects
{
    public abstract record MeteoParameter
    {
        public DateOnly Date { get; set; }
        public double? Value { get; set; }
    }

    public record TemperatureParameter : MeteoParameter { }
    public record MinTemperatureParameter : MeteoParameter { }
    public record MaxTemperatureParameter : MeteoParameter { }
    public record RainfallParameter : MeteoParameter { }
    public record SnowHightParameter : MeteoParameter { }
    public record HumidityParameter : MeteoParameter { }
}
