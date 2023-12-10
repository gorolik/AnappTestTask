using System.Collections.Generic;
using System.Linq;
using Sources.Services.PersistentProgress.Services;
using UnityEngine;

namespace Sources.UI.Windows.Shop
{
    public abstract class ShopSection : MonoBehaviour
    {
        [SerializeField] private ShopItemData[] _items;
        [SerializeField] private ShopItem _itemPrefab;
        [SerializeField] private Transform _parent;

        protected IPersistentProgressService ProgressService;
        
        private List<ShopItem> _shopItems = new List<ShopItem>();

        public void Construct(IPersistentProgressService progressService) =>
            ProgressService = progressService;
        
        public void Init() => 
            DisplayGoods();

        public void Cleanup()
        {
            foreach (ShopItem item in _shopItems)
            {
                item.BuyButtonClicked -= OnBuyButtonClicked;
                Destroy(item.gameObject);
            }
        }

        private void DisplayGoods()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                var data = ProgressService.ProgressContainer.PersistentData;
                
                bool isOpened = data.CompletedLevel + 1 >= _items[i].LevelRequired;
                bool isPurchased = IsPurchased(i);
                
                ShopItem itemInstance = Instantiate(_itemPrefab, _parent);
                itemInstance.Init(i, _items[i].Name, _items[i].Tickets, _items[i].Icon);

                if(!isOpened)
                    itemInstance.CloseItem(_items[i].LevelRequired);
                else if(isPurchased)
                    itemInstance.Purchased();
                
                itemInstance.BuyButtonClicked += OnBuyButtonClicked;
                _shopItems.Add(itemInstance);
            }
        }

        private void OnBuyButtonClicked(int id)
        {
            var data = ProgressService.ProgressContainer.PersistentData;
            
            if(data.Tickets < _items[id].Tickets)
                return;
            
            data.AddTickets(-_items[id].Tickets);
            OnPurchased(id);

            ShopItem shopItem = _shopItems.FirstOrDefault(x => x.ID == id);
            shopItem.Purchased();
        }
        
        protected abstract void OnPurchased(int id);
        protected abstract bool IsPurchased(int id);
    }
}