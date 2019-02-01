namespace Checkout
{
    public interface ICheckoutLineItem
    {
        char Item { get; }
        int Count { get; }
    }
}
