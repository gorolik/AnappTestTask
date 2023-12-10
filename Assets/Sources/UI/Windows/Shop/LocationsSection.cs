using System.Linq;

namespace Sources.UI.Windows.Shop
{
    public class LocationsSection : ShopSection
    {
        protected override void OnPurchased(int id)
        {
            var data = ProgressService.ProgressContainer.PersistentData;
            data.AddItem(ref data.Locations, id);
        }

        protected override bool IsPurchased(int id)
        {
            int[] purchasedLocations = ProgressService.ProgressContainer.PersistentData.Locations;
            return purchasedLocations.Contains(id);
        }
    }
}