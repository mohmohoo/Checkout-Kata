namespace Checkout
{
    public interface IValueHolder<TValue>
    {
        TValue Value { get; }
    }
}
