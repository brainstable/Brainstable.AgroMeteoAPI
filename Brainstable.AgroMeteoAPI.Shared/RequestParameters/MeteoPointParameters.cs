namespace Brainstable.AgroMeteoAPI.Shared.RequestParameters;

public class MeteoPointParameters : RequestParameters
{
    public DateOnly MinDate { get; set; }
    public DateOnly MaxDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public bool ValidDateRange => MaxDate > MinDate;
}