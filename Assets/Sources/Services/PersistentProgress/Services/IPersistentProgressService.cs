using Sources.Infrastructure.DI;
using Sources.Services.PersistentProgress.Structure;

namespace Sources.Services.PersistentProgress.Services
{
    public interface IPersistentProgressService : IService
    {
        void SaveProgress();
        PersistentData LoadProgress();
        IPersistentProgressContainer ProgressContainer { get; }
    }
}