using System.Collections.Generic;
using Sources.Services.IAP;
using UnityEngine;

namespace Sources.UI.Windows.Shop
{
    public class TicketsSection : MonoBehaviour
    {
        [SerializeField] private TicketsShopItem _ticketsShopItemPrefab;
        [SerializeField] private Transform _parent;

        private IIAPService _iapService;

        private List<TicketsShopItem> _ticketsShopItems = new List<TicketsShopItem>();

        public void Construct(IIAPService iapService) => 
            _iapService = iapService;

        public void Init() => 
            DisplayGoods();

        public void Cleanup()
        {
            foreach (TicketsShopItem item in _ticketsShopItems)
            {
                item.BuyButtonClicked -= OnBuyButtonClicked;
                Destroy(item.gameObject);
            }
        }

        private void DisplayGoods()
        {
            foreach (ProductDescription product in _iapService.Products())
            {
                TicketsShopItem item = Instantiate(_ticketsShopItemPrefab, _parent);
                item.Init(product.Id, product.Config.Price, product.Config.Quantity, product.Config.Icon, product.Config.Name);

                item.BuyButtonClicked += OnBuyButtonClicked;
                _ticketsShopItems.Add(item);
            }
        }

        private void OnBuyButtonClicked(string productId) => 
            _iapService.StartPurchase(productId);
    }
}