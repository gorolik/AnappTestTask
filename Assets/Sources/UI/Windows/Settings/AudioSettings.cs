using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Sources.UI.Windows.Settings
{
    public class AudioSettings : MonoBehaviour
    {
        [SerializeField] private string _saveKey;
        [SerializeField] private string _parameter = "Parameter";
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Button _switchButton;
        [SerializeField] private GameObject _offObject;

        private void Start() =>
            SyncState();

        private void OnEnable() => 
            _switchButton.onClick.AddListener(OnSwitchButtonClicked);

        private void OnDisable() => 
            _switchButton.onClick.RemoveListener(OnSwitchButtonClicked);

        private void SyncState()
        {
            if (PlayerPrefs.GetInt(_saveKey) == 1)
                Enable();
            else
                Disable();
        }

        private void OnSwitchButtonClicked()
        {
            if (PlayerPrefs.GetInt(_saveKey) == 1)
                Disable();
            else
                Enable();
        }

        private void Disable()
        {
            _offObject.SetActive(true);
            _mixer.SetFloat(_parameter, -80);
            
            PlayerPrefs.SetInt(_saveKey, 0);
        }

        private void Enable()
        {
            _offObject.SetActive(false);
            _mixer.SetFloat(_parameter, 0);

            PlayerPrefs.SetInt(_saveKey, 1);
        }
    }
}