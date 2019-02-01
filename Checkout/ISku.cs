namespace Checkout
{
    public interface ISku
    {
        char Name { get; }
        int GetPrice(IItemCount items);
    }
}
