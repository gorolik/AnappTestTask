using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonClickSoundSource : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volume = 0.5f;
        
        private Button _button;
        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponentInParent<AudioSource>();
            _button = GetComponent<Button>();
        }

        private void OnEnable() => 
            _button.onClick.AddListener(OnButtonClicked);

        private void OnDisable() => 
            _button.onClick.RemoveListener(OnButtonClicked);
        
        private void OnButtonClicked() => 
            _source.PlayOneShot(_clip, _volume);
    }
}