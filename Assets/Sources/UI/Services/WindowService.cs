using Sources.UI.Factory;
using UnityEngine;

namespace Sources.UI.Services
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _uiFactory;

        public WindowService(IUIFactory uiFactory) => 
            _uiFactory = uiFactory;

        public void Open(WindowId id)
        {
            switch (id)
            {
                case WindowId.MainMenu:
                    _uiFactory.CreateMainMenu(this);
                    break;
                case WindowId.LevelSelection:
                    _uiFactory.CreateLevelSelectionWindow(this);
                    break;
                case WindowId.DailyBonus:
                    _uiFactory.CreateDailyBonusWindow();
                    break;
                case WindowId.Settings:
                    _uiFactory.CreateSettingsWindow();
                    break;
                case WindowId.Shop:
                    _uiFactory.CreateShopWindow(this);
                    break;
                default:
                    Debug.LogError("Unknown window type");
                    break;
            }
        }
    }
}