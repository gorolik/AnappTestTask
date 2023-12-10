using Sources.Infrastructure.AssetManagement;
using Sources.Infrastructure.DI;
using Sources.Services.DataFormatters;
using Sources.Services.IAP;
using Sources.Services.PersistentProgress;
using Sources.Services.PersistentProgress.Services;
using Sources.Services.PersistentProgress.Services.DataSavers;
using Sources.Services.PersistentProgress.Structure;
using Sources.Services.StaticData;
using Sources.UI;
using Sources.UI.Factory;
using Sources.UI.Services;
using Unity.Services.Core;
using UnityEngine;

namespace Sources.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private AllServices _container;
        
        private void Start()
        {
            _container = AllServices.Container;

            InitUnityServices();
                
            RegisterServices();
            LoadPersistentData();
            
            CreateStartMenu();
            
            DontDestroyOnLoad(this);
        }

        private void LoadPersistentData()
        {
            IPersistentProgressContainer progressContainer = _container.Single<IPersistentProgressContainer>();
            IPersistentProgressService progressService = _container.Single<IPersistentProgressService>();

            PersistentData persistentData = progressService.LoadProgress();
            
            if (persistentData == null)
                persistentData = new PersistentData();

            progressContainer.PersistentData = persistentData;
        }

        private void CreateStartMenu()
        {
            IUIFactory uiFactory = _container.Single<IUIFactory>();
            IWindowService windowService = _container.Single<IWindowService>();
            
            uiFactory.CreateUIRoot();
            windowService.Open(WindowId.MainMenu);
        }

        private async void InitUnityServices() => 
            await UnityServices.InitializeAsync();

        private void RegisterServices()
        {
            _container.RegisterSingle<IDataFormatter>(new JsonDataFormatter());
            _container.RegisterSingle<IDataSaver>(new FileSaver());
            
            RegisterProgressService();

            RegisterStaticDataService();
            RegisterIAPService(new IAPProvider(), _container.Single<IPersistentProgressService>());

            _container.RegisterSingle<IAssets>(new AssetProvider());
            
            RegisterUIFactory();
            
            _container.RegisterSingle<IWindowService>(new WindowService(_container.Single<IUIFactory>()));
        }

        private void RegisterProgressService()
        {
            _container.RegisterSingle<IPersistentProgressContainer>(new PersistentProgressContainer());
            _container.RegisterSingle<IPersistentProgressService>(new PersistentProgressService(
                _container.Single<IPersistentProgressContainer>(),
                _container.Single<IDataFormatter>(),
                _container.Single<IDataSaver>()));
        }

        private void RegisterUIFactory()
        {
            _container.RegisterSingle<IUIFactory>(new UIFactory(
                _container.Single<IAssets>(),
                _container.Single<IStaticDataService>(),
                _container.Single<IIAPService>(),
                _container.Single<IPersistentProgressService>()));
        }

        private void RegisterStaticDataService()
        {
            StaticDataService staticDataService = new StaticDataService();
            _container.RegisterSingle<IStaticDataService>(staticDataService);
            
            staticDataService.LoadData();
        }

        private void RegisterIAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
        {
            IAPService iapService = new IAPService(iapProvider, progressService);
            _container.RegisterSingle<IIAPService>(iapService);
            
            iapService.Init();
        }
    }
}
