using Sources.Infrastructure.DI;
using Sources.StaticData.UI;
using Sources.UI;

namespace Sources.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadData();
        WindowConfig GetWindowById(WindowId id);
    }
}