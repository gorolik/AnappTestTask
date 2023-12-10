using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Services.DataFormatters;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Product = UnityEngine.Purchasing.Product;

namespace Sources.Services.IAP
{
    public class IAPProvider : IDetailedStoreListener
    {
        private const string IAPConfigsPath = "IAP/Products";
        
        private IStoreController _controller;
        private IExtensionProvider _extensions;
        private IAPService _iapService;

        public Dictionary<string, ProductConfig> Configs { get; private set; }
        public Dictionary<string, Product> Products { get; private set; }
        
        public event Action Initialized;

        public bool IsInitialized => _controller != null && _extensions != null;
        
        public void Init(IAPService iapService)
        {
            _iapService = iapService;
            
            Configs = new Dictionary<string, ProductConfig>();
            Products = new Dictionary<string, Product>();
            
            Load();
            
            ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            
            foreach (ProductConfig productConfig in Configs.Values) 
                builder.AddProduct(productConfig.Id, productConfig.ProductType);

            UnityPurchasing.Initialize(this, builder);
        }

        public void StartPurchase(string productId) => 
            _controller.InitiatePurchase(productId);

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            _controller = controller;
            _extensions = extensions;

            foreach (Product product in _controller.products.all) 
                Products.Add(product.definition.id, product);

            Initialized?.Invoke();
            
            Debug.Log("IAP Initialized");
        }

        public void OnInitializeFailed(InitializationFailureReason error) => 
            Debug.Log($"IAP Initialize failed: {error}");

        public void OnInitializeFailed(InitializationFailureReason error, string message) => 
            Debug.Log($"IAP Initialize failed: {error}\n{message}");

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            Debug.Log($"Product: {purchaseEvent.purchasedProduct.definition.id} purchase success");
            
            return _iapService.ProcessPurchase(purchaseEvent.purchasedProduct);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason) => 
            Debug.Log($"Product: {product.definition.id} purchase failed, reason {failureReason}, transaction id: {product.transactionID}");

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription) => 
            Debug.Log($"Product: {product.definition.id} purchase failed, description {failureDescription.message}, transaction id: {product.transactionID}");

        private void Load()
        {
            IDataFormatter dataFormatter = new JsonDataFormatter();
            
            Configs = dataFormatter
                .Deserialize<ProductConfigWrapper>(Resources
                    .Load<TextAsset>(IAPConfigsPath).text).Configs
                    .ToDictionary(x => x.Id, x => x);
        }
    }
}