using System;

namespace Checkout.Store
{
    public class ItemCount
        : IItemCount
    {
        public int Value { get; }

        public ItemCount(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Invalid item count value", "value");
            }

            Value = value;
        }
    }
}
