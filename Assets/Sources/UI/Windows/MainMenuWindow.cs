using Sources.Services.PersistentProgress.Services;
using Sources.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows
{
    public class MainMenuWindow : WindowBase
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _dailyBonusButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private TMP_Text _ticketsCount;
        
        private IWindowService _windowService;
        private IPersistentProgressService _progressService;

        public void Construct(IWindowService windowService, IPersistentProgressService progressService)
        {
            _windowService = windowService;
            _progressService = progressService;
        }

        protected override void Init() => 
            _ticketsCount.text = _progressService.ProgressContainer.PersistentData.Tickets.ToString();

        protected override void SubscribeUpdates()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
            _dailyBonusButton.onClick.AddListener(OnDailyBonusButtonClick);
            _settingsButton.onClick.AddListener(OnSettingsButtonClick);
            _shopButton.onClick.AddListener(OnShopButtonClick);

            _progressService.ProgressContainer.PersistentData.TicketsCountChanged += OnTicketsCountChanged;
        }

        protected override void Cleanup()
        {
            _playButton.onClick.RemoveListener(OnPlayButtonClick);
            _dailyBonusButton.onClick.RemoveListener(OnDailyBonusButtonClick);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
            _shopButton.onClick.RemoveListener(OnShopButtonClick);
            
            _progressService.ProgressContainer.PersistentData.TicketsCountChanged -= OnTicketsCountChanged;
        }

        private void OnTicketsCountChanged(int value) => 
            _ticketsCount.text = value.ToString();

        private void OnPlayButtonClick()
        {
            _windowService.Open(WindowId.LevelSelection);
            Close();
        }

        private void OnDailyBonusButtonClick() => 
            _windowService.Open(WindowId.DailyBonus);

        private void OnSettingsButtonClick() => 
            _windowService.Open(WindowId.Settings);

        private void OnShopButtonClick()
        {
            Close();
            _windowService.Open(WindowId.Shop);
        }
    }
}