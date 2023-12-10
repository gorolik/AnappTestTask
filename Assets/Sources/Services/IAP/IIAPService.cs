using System;
using System.Collections.Generic;
using Sources.Infrastructure.DI;

namespace Sources.Services.IAP
{
    public interface IIAPService : IService
    {
        bool IsInitialized { get; }
        event Action Initialized;
        void Init();
        List<ProductDescription> Products();
        void StartPurchase(string productId);
    }
}