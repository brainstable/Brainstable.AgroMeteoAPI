namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class MeteoStationCollectionBadRequestException: BadRequestException
{
    public MeteoStationCollectionBadRequestException()
        : base("MeteoStation collection sent from a client is null") { }
}