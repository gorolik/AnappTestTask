using System;
using Sources.Infrastructure.DI;
using Sources.UI.Services;

namespace Sources.UI.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        void CreateMainMenu(IWindowService windowService);
        void CreateLevelSelectionWindow(IWindowService windowService);
        void CreateDailyBonusWindow();
        void CreateSettingsWindow();
        void CreateShopWindow(IWindowService windowService);
    }
}