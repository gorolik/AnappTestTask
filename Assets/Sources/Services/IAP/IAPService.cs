using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Infrastructure.DI;
using Sources.Services.PersistentProgress.Services;
using UnityEngine.Purchasing;

namespace Sources.Services.IAP
{
    public class IAPService : IIAPService
    {
        private readonly IAPProvider _iapProvider;
        private readonly IPersistentProgressService _progressService;

        public bool IsInitialized => _iapProvider.IsInitialized;
        public event Action Initialized;
        
        public IAPService(IAPProvider iapProvider, IPersistentProgressService progressService)
        {
            _iapProvider = iapProvider;
            _progressService = progressService;
        }

        public void Init()
        {
            _iapProvider.Init(this);
            _iapProvider.Initialized += () => Initialized?.Invoke();
        }

        public List<ProductDescription> Products() =>
            ProductDescriptions().ToList();

        private IEnumerable<ProductDescription> ProductDescriptions()
        {
            foreach (string productId in _iapProvider.Products.Keys)
            {
                ProductConfig config = _iapProvider.Configs[productId];
                Product product = _iapProvider.Products[productId];

                yield return new ProductDescription
                {
                    Id = productId,
                    Config = config,
                    Product = product
                };
            }
        }

        public void StartPurchase(string productId) => 
            _iapProvider.StartPurchase(productId);

        public PurchaseProcessingResult ProcessPurchase(Product purchaseProduct)
        {
            ProductConfig productConfig = _iapProvider.Configs[purchaseProduct.definition.id];

            switch (productConfig.ItemType)
            {
                case ItemType.Tickets:
                    _progressService.ProgressContainer.PersistentData.AddTickets(productConfig.Quantity);
                    _progressService.SaveProgress();
                    break;
            }
            
            return PurchaseProcessingResult.Complete;
        }
    }
}