using Sources.Infrastructure.DI;

namespace Sources.Services.DataFormatters
{
    public interface IDataFormatter : IService
    {
        string Serialize(object obj);
        T Deserialize<T>(string data);
    }
}