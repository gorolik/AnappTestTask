using Sources.Services.IAP;
using Sources.Services.PersistentProgress.Services;
using Sources.UI.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.Shop
{
    public class ShopWindow : WindowBase
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private TMP_Text _ticketsCount;
        [SerializeField] private TicketsSection _ticketsSection;
        [SerializeField] private SkinsSection _skinsSection;
        [SerializeField] private LocationsSection _locationsSection;

        private IWindowService _windowService;
        private IIAPService _iapService;
        private IPersistentProgressService _progressService;

        public void Construct(IWindowService windowService, IIAPService iapService, IPersistentProgressService progressService)
        {
            _windowService = windowService;
            _iapService = iapService;
            _progressService = progressService;
        }

        protected override void Init()
        {
            _ticketsSection.Construct(_iapService);
            _ticketsSection.Init();
            
            _skinsSection.Construct(_progressService);
            _skinsSection.Init();
            
            _locationsSection.Construct(_progressService);
            _locationsSection.Init();
            
            OnTicketCountChanged(_progressService.ProgressContainer.PersistentData.Tickets);
        }

        protected override void SubscribeUpdates()
        {
            _homeButton.onClick.AddListener(OnHomeButtonClicked);
            _progressService.ProgressContainer.PersistentData.TicketsCountChanged += OnTicketCountChanged;
        }

        protected override void Cleanup()
        {
            _homeButton.onClick.AddListener(OnHomeButtonClicked);
            _progressService.ProgressContainer.PersistentData.TicketsCountChanged -= OnTicketCountChanged;
            
            _ticketsSection.Cleanup();
            _skinsSection.Cleanup();
            _locationsSection.Cleanup();
        }

        private void OnHomeButtonClicked()
        {
            Close();
            _windowService.Open(WindowId.MainMenu);
        }

        private void OnTicketCountChanged(int value) => 
            _ticketsCount.text = value.ToString();
    }
}