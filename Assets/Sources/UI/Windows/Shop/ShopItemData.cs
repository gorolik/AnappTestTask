using System;
using UnityEngine;

namespace Sources.UI.Windows.Shop
{
    [Serializable]
    public class ShopItemData
    {
        [SerializeField] private string _name;
        [SerializeField] private int _levelRequired;
        [SerializeField] private int _tickets;
        [SerializeField] private Sprite _icon;

        public string Name => _name;
        public int LevelRequired => _levelRequired;
        public int Tickets => _tickets;
        public Sprite Icon => _icon;
    }
}