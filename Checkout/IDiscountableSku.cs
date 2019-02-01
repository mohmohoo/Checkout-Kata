namespace Checkout
{
    public interface IDiscountableSku
        : ISku
    {
        int UnitPrice { get; }
    }
}
