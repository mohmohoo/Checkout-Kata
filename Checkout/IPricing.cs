using System.Collections.ObjectModel;

namespace Checkout
{
    public interface IPricing
    {
        ReadOnlyCollection<ISku> AvailableSkus { get; }

        bool TryGetSku(string item, out ISku sku);
    }
}
