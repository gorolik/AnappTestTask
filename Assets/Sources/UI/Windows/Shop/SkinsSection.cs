using System.Linq;

namespace Sources.UI.Windows.Shop
{
    public class SkinsSection : ShopSection
    {
        protected override void OnPurchased(int id)
        {
            var data = ProgressService.ProgressContainer.PersistentData;
            data.AddItem(ref data.Skins, id);
        }

        protected override bool IsPurchased(int id)
        {
            int[] purchasedSkins = ProgressService.ProgressContainer.PersistentData.Skins;
            return purchasedSkins.Contains(id);
        }
    }
}