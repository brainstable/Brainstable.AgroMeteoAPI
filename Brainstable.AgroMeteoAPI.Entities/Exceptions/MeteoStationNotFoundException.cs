namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class MeteoStationNotFoundException : NotFoundException
{
    public MeteoStationNotFoundException(string meteoStationId) 
        : base($"The meteostation with id: {meteoStationId} doesn't exist in the database")
    {
    }
}