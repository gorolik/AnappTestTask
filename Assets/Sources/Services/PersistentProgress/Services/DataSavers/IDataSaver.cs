using Sources.Infrastructure.DI;

namespace Sources.Services.PersistentProgress.Services.DataSavers
{
    public interface IDataSaver : IService
    {
        string GetData();
        void Save(string data);
    }
}