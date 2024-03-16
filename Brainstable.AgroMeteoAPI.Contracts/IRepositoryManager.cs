namespace Brainstable.AgroMeteoAPI.Contracts
{
    public interface IRepositoryManager
    {
        IMeteoStationRepository MeteoStation {  get; }
        IMeteoPointRepository MeteoPoint { get; }
        Task SaveAsync();
    }
}
