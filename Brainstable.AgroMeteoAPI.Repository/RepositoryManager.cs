using Brainstable.AgroMeteoAPI.Contracts;

namespace Brainstable.AgroMeteoAPI.Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext repositoryContext;
        private readonly Lazy<IMeteoStationRepository> meteoStationRepository;
        private readonly Lazy<IMeteoPointRepository> meteoPointRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
            this.meteoStationRepository = new Lazy<IMeteoStationRepository>(() => new MeteoStationRepository(repositoryContext));
            this.meteoPointRepository = new Lazy<IMeteoPointRepository>(() => new MeteoPointRepository(repositoryContext));
        }

        public IMeteoStationRepository MeteoStation => meteoStationRepository.Value;

        public IMeteoPointRepository MeteoPoint => meteoPointRepository.Value;

        public Task SaveAsync() => repositoryContext.SaveChangesAsync();
    }
}
