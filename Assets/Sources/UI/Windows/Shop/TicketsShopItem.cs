using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.UI.Windows.Shop
{
    public class TicketsShopItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _quantity;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private Button _buyButton;

        private string _productId;

        public event Action<string> BuyButtonClicked;

        public void Init(string productId, string price, int quantity, string icon, string name)
        {
            _productId = productId;
            _price.text = price;
            _quantity.text = $"x{quantity}";
            _name.text = name;

            //_icon.sprite = Resources.Load<Sprite>(icon);
        }

        private void OnEnable() => 
            _buyButton.onClick.AddListener(OnBuyButtonClicked);
        
        private void OnDisable() => 
            _buyButton.onClick.RemoveListener(OnBuyButtonClicked);

        private void OnBuyButtonClicked() => 
            BuyButtonClicked?.Invoke(_productId);
    }
}