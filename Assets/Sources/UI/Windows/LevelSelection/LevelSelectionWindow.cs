using Sources.Services.PersistentProgress.Services;
using Sources.UI.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.LevelSelection
{
    public class LevelSelectionWindow : WindowBase
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private LevelsList _levelsList;
        
        private IWindowService _windowService;
        private IPersistentProgressService _progressService;

        public void Construct(IWindowService windowService, IPersistentProgressService progressService)
        {
            _windowService = windowService;
            _progressService = progressService;
        }

        protected override void Init()
        {
            _levelsList.Construct(_progressService);
            _levelsList.Init();
        }

        protected override void SubscribeUpdates() => 
            _homeButton.onClick.AddListener(OnHomeButtonClick);

        protected override void Cleanup()
        {
            _homeButton.onClick.RemoveListener(OnHomeButtonClick);
            
            _levelsList.Cleanup();
        }

        private void OnHomeButtonClick()
        {
            Close();
            _windowService.Open(WindowId.MainMenu);
        }
    }
}