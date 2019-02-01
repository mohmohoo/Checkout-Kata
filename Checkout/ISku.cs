using Checkout.Store;

namespace Checkout
{
    public interface ISku
    {
        int GetPrice(ItemCount items);
    }
}
