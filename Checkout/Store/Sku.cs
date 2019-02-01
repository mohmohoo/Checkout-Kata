using System;
namespace Checkout.Store
{
    public class Sku : ISku
    {
        private readonly char _name;
        private readonly int _unitPrice;
        public Sku(char name, int unitPrice)
        {
            _name = name;
            _unitPrice = unitPrice;
        }

        public int GetPrice(ItemCount items)
        {
            throw new NotImplementedException();
        }
    }
}
