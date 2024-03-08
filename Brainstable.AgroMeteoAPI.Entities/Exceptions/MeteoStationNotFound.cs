namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class MeteoStationNotFound : NotFoundException
{
    public MeteoStationNotFound(string meteoStationId) 
        : base($"The meteostation with id: {meteoStationId} doesn't exist in the database")
    {
    }
}