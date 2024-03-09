namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class MeteoPointNotFound : NotFoundException
{
    public MeteoPointNotFound(string meteoStationId, DateOnly date)
        : base($"The meteopoint with id: {meteoStationId} and date: {date} doesn't exist in the database")
    {
    }
}