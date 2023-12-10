using System;
using Sources.Extensions;
using Sources.Services.PersistentProgress.Services;
using Sources.Services.PersistentProgress.Structure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.DailyBonus
{
    public class DailyBonusWindow : WindowBase
    {
        private readonly TimeSpan _dailyResetTime = new TimeSpan(24, 0, 0);
        
        [SerializeField] private CanvasGroup _overview;
        [SerializeField] private CanvasGroup _receiving;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TMP_Text _receiveBonusValue;
        [SerializeField] private TMP_Text _receiveDay;
        [SerializeField] private Slider _progressSlider;
        [SerializeField] private TMP_Text _progressText;
        [SerializeField] private int[] _bonuses = new int[7];

        private IPersistentProgressService _progressService;

        public void Construct(IPersistentProgressService progressService) => 
            _progressService = progressService;

        protected override void Init()
        {
            ShouldGetDailyBonus();

            if (ShouldGetDailyBonus())
            {
                ReceiveBonus();
                _receiving.ShowGroup();
            }
            else
                _overview.ShowGroup();

            UpdateProgressView();
        }

        protected override void SubscribeUpdates() => 
            _closeButton.onClick.AddListener(OnCloseButtonClicked);

        protected override void Cleanup() => 
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);

        private void ReceiveBonus()
        {
            PersistentData data = _progressService.ProgressContainer.PersistentData;
            int day = data.BonusDay;

            if (day >= _bonuses.Length)
            {
                day = 0;
                data.BonusDay = 0;
            }

            _receiveBonusValue.text = $"x{_bonuses[day]}";
            
            data.AddTickets(_bonuses[day]);
            data.BonusDay++;
            data.LastDailyBonusData = DateTime.Now.ToString();
            
            _receiveDay.text = $"Day {day}";
            
            _progressService.SaveProgress();
        }

        private bool ShouldGetDailyBonus()
        {
            DateTime lastBonusTime = DateTime.Parse(_progressService.ProgressContainer.PersistentData.LastDailyBonusData);
            DateTime now = DateTime.Now;
            
            TimeSpan timeDifference = now - lastBonusTime;

            return timeDifference >= _dailyResetTime;
        }

        private void UpdateProgressView()
        {
            int day = _progressService.ProgressContainer.PersistentData.BonusDay;
            int maxDays = _bonuses.Length + 1;

            _progressSlider.maxValue = maxDays;
            _progressSlider.value = day;
            _progressText.text = $"{day}/{maxDays}";
        }

        private void OnCloseButtonClicked() => 
            Close();
    }
}