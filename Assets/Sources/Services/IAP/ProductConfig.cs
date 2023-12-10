using System;
using UnityEngine.Purchasing;

namespace Sources.Services.IAP
{
    [Serializable]
    public class ProductConfig
    {
        public string Id;
        public ProductType ProductType;

        public ItemType ItemType;
        public string Name;
        public int Quantity;
        public string Price;
        public string Icon;
    }
}