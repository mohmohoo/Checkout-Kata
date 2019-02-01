using System.Collections.ObjectModel;

namespace Checkout
{
    public interface IPricing
    {
        ReadOnlyCollection<ISku> AvailableSkus { get; }

        int GetPrice(int itemCount);
    }
}
