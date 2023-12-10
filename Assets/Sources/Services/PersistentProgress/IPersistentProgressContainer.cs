using Sources.Infrastructure.DI;
using Sources.Services.PersistentProgress.Structure;

namespace Sources.Services.PersistentProgress
{
    public interface IPersistentProgressContainer : IService
    {
        PersistentData PersistentData { get; set; }
    }
}