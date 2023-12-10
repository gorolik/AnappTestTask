using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.Shop
{
    public class ShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _buyButton;
        [Header("Closed")]
        [SerializeField] private GameObject _closed;
        [SerializeField] private TMP_Text _levelRequired;
        [Header("Purchased")] 
        [SerializeField] private GameObject _purchasedCheckmark;
        [SerializeField] private GameObject _ticketsIcon;

        private int _id;
        private bool _purchased;
        private bool _itemClosed;

        public int ID => _id;

        public event Action<int> BuyButtonClicked;

        public void Init(int id, string name, int price, Sprite icon)
        {
            _id = id;
            _price.text = price.ToString();
            _name.text = name;
            _icon.sprite = icon;
        }

        public void CloseItem(int levelRequired)
        {
            _closed.SetActive(true);
            _icon.gameObject.SetActive(false);
            _levelRequired.text = $"LV. {levelRequired}";

            _itemClosed = true;
        }

        public void Purchased()
        {
            _price.gameObject.SetActive(false);
            _ticketsIcon.gameObject.SetActive(false);
            _purchasedCheckmark.SetActive(true);

            _purchased = true;
        }

        private void OnEnable() => 
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        
        private void OnDisable() => 
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);

        private void OnBuyButtonClicked()
        {
            if(_purchased || _itemClosed)
                return;
            
            BuyButtonClicked?.Invoke(_id);
        }
    }
}