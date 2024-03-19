namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class MeteoPointNotFoundException : NotFoundException
{
    public MeteoPointNotFoundException(string meteoStationId, DateOnly date)
        : base($"The meteopoint with id: {meteoStationId} and date: {date} doesn't exist in the database")
    {
    }
}