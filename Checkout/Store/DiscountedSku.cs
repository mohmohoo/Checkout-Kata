using System;

namespace Checkout.Store
{
    public class DiscountedSku : ISku
    {
        private readonly int _specialPriceItemCount;
        private readonly int _speicalPrice;
        private readonly Sku _sku;

        public DiscountedSku(Sku sku, int specialPriceItemCount, int specialPrice)
        {
            _sku = sku;
            _specialPriceItemCount = specialPriceItemCount;
            _speicalPrice = specialPrice;
        }

        public int GetPrice(ItemCount items)
        {
            throw new NotImplementedException();
        }
    }
}
