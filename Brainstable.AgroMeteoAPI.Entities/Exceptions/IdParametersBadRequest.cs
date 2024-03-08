namespace Brainstable.AgroMeteoAPI.Entities.Exceptions;

public sealed class IdParametersBadRequest : BadRequestException
{
    public IdParametersBadRequest()
        : base("Parameter ids is null") { }
}