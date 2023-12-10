using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.Settings
{
    public class SettingsWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;

        protected override void SubscribeUpdates() => 
            _closeButton.onClick.AddListener(OnCloseButtonClicked);

        protected override void Cleanup() => 
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);

        private void OnCloseButtonClicked() => 
            Close();
    }
}