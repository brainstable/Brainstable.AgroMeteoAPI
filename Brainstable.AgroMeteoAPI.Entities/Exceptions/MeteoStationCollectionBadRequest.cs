namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class MeteoStationCollectionBadRequest: BadRequestException
{
    public MeteoStationCollectionBadRequest()
        : base("MeteoStation collection sent from a client is null") { }
}