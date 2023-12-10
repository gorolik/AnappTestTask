using System;
using Sources.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.LevelSelection
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _selectButton;
        [SerializeField] private TMP_Text _number;
        [SerializeField] private CanvasGroup _opened;
        [SerializeField] private CanvasGroup _closed;

        private int _levelId;

        public event Action<int> Selected; 

        public void Init(int levelId, bool isOpened)
        {
            _levelId = levelId;

            _number.text = (levelId + 1).ToString();
            
            if(isOpened)
                _opened.ShowGroup();
            else
                _closed.ShowGroup();
        }

        private void OnEnable() => 
            _selectButton.onClick.AddListener(OnSelectButtonClicked);

        private void OnDisable() => 
            _selectButton.onClick.RemoveListener(OnSelectButtonClicked);
        
        private void OnSelectButtonClicked() => 
            Selected?.Invoke(_levelId);
    }
}