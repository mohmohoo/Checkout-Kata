﻿using System;

namespace Checkout.Store
{
    public class DiscountedSku : ISku
    {
        private readonly int _specialPriceItemCount;
        private readonly int _speicalPrice;
        private readonly IDiscountableSku _sku;

        public char Name
        {
            get => _sku.Name;
        }

        public DiscountedSku(IDiscountableSku sku, int specialPriceItemCount, int specialPrice)
        {
            _sku = sku ?? throw new ArgumentException("Discounted sku cannot be null");

            if (specialPrice < sku.UnitPrice)
            {
                throw new ArgumentException("Special price cannot be less than unit price");
            }

            _specialPriceItemCount = specialPriceItemCount > 0 
                ? specialPriceItemCount 
                : throw new ArgumentException("Special price sku count is not valid", "specialPriceItemCount");

            _speicalPrice = specialPrice > 0
                ? specialPrice
                : throw new ArgumentException("Special price is not valid", "specialPrice");
        }

        public int GetPrice(IItemCount items)
        {
            var skuPrice = 0;
            var unitPriceItemCount = items.Value % _specialPriceItemCount; 
            if (unitPriceItemCount > 0)
            {
                skuPrice = _sku.GetPrice(new ItemCount(unitPriceItemCount));
            }

            var discountedItemCount = items.Value / _specialPriceItemCount;
            return (discountedItemCount * _speicalPrice) + skuPrice;
        }
    }
}
