using Sources.Infrastructure.AssetManagement;
using Sources.Services.IAP;
using Sources.Services.PersistentProgress.Services;
using Sources.Services.StaticData;
using Sources.UI.Services;
using Sources.UI.Windows;
using Sources.UI.Windows.DailyBonus;
using Sources.UI.Windows.LevelSelection;
using Sources.UI.Windows.Shop;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Sources.UI.Factory
{
    public class UIFactory : IUIFactory
    {
        private readonly IAssets _assets;
        private readonly IStaticDataService _staticData;
        private readonly IIAPService _iapService;
        private readonly IPersistentProgressService _progressService;

        private Transform _uiRoot;

        public UIFactory(IAssets assets, IStaticDataService staticData, IIAPService iapService, IPersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _iapService = iapService;
            _progressService = progressService;
        }

        public void CreateUIRoot()
        {
            Transform uiRoot = Object.Instantiate(_assets.GetPrefabByPath(AssetsPath.UIRootPath)).transform;
            _uiRoot = uiRoot;
        }

        public void CreateMainMenu(IWindowService windowService)
        {
            WindowBase window = InstantiateByWindowId(WindowId.MainMenu);
            
            MainMenuWindow mainMenuWindow = window as MainMenuWindow;
            mainMenuWindow.Construct(windowService, _progressService);
        }

        public void CreateLevelSelectionWindow(IWindowService windowService)
        {
            WindowBase window = InstantiateByWindowId(WindowId.LevelSelection);
            
            LevelSelectionWindow levelSelectionWindow = window as LevelSelectionWindow;
            levelSelectionWindow.Construct(windowService, _progressService);
        }
        
        public void CreateDailyBonusWindow()
        {
            WindowBase window = InstantiateByWindowId(WindowId.DailyBonus);
            
            DailyBonusWindow dailyBonusWindow = window as DailyBonusWindow;
            dailyBonusWindow.Construct(_progressService);
        }
        
        public void CreateSettingsWindow() => 
            InstantiateByWindowId(WindowId.Settings);

        public void CreateShopWindow(IWindowService windowService)
        {
            WindowBase window = InstantiateByWindowId(WindowId.Shop);
            
            ShopWindow shopWindow = window as ShopWindow;
            shopWindow.Construct(windowService, _iapService, _progressService);
        }

        private WindowBase InstantiateByWindowId(WindowId id) => 
            Object.Instantiate(_staticData.GetWindowById(id).Prefab, _uiRoot);
    }
}