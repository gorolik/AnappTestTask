using Sources.Services.DataFormatters;
using Sources.Services.PersistentProgress.Services.DataSavers;
using Sources.Services.PersistentProgress.Structure;

namespace Sources.Services.PersistentProgress.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        private readonly IPersistentProgressContainer _progressContainer;
        private readonly IDataFormatter _dataFormatter;
        private readonly IDataSaver _dataSaver;

        public IPersistentProgressContainer ProgressContainer => _progressContainer;

        public PersistentProgressService(IPersistentProgressContainer progressContainer, IDataFormatter dataFormatter, IDataSaver dataSaver)
        {
            _progressContainer = progressContainer;
            _dataFormatter = dataFormatter;
            _dataSaver = dataSaver;
        }

        public void SaveProgress()
        {
            string data = _dataFormatter.Serialize(_progressContainer.PersistentData);
            _dataSaver.Save(data);
        }

        public PersistentData LoadProgress() => 
            _dataFormatter.Deserialize<PersistentData>(_dataSaver.GetData());
    }
}