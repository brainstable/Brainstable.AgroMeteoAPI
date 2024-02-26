namespace Brainstable.AgroMeteoAPI.Service.Contracts
{
    public interface IServiceManager 
    { 
        IMeteoStationService MeteoStationService { get; }
        IMeteoPointService MeteoPointService { get; }
    }
}
