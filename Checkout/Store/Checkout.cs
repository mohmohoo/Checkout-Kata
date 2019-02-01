using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Store
{
    public class Checkout
        : ICheckout
    {
        private readonly IPricing _pricing;
        private readonly List<LineItem> _lineItems;

        public Checkout(IPricing pricing)
        {
            _pricing = pricing ?? throw new ArgumentException("Invalid null value", "pricing");
            _lineItems = new List<LineItem>();
        }

        public int GetTotalPrice() => _lineItems.Sum(lineItem => lineItem.GetPriceFunc());

        public void Scan(string item)
        {
            var lineItem = _lineItems.SingleOrDefault(l => l.Item.Equals(item, StringComparison.InvariantCultureIgnoreCase));
            if (lineItem != null)
            {
                lineItem.ItemCount += 1;
            }
            else
            {
                if (!_pricing.TryGetSku(item, out ISku sku))
                {
                    throw new ArgumentException("Invalid item", "item");
                }

                var newLineItem = new LineItem(item, itemCount => sku.GetPrice(new ItemCount(itemCount)))
                {
                    ItemCount = 0
                };
                _lineItems.Add(newLineItem);
            }
        }

        private class LineItem
        {
            public string Item { get; }
            public int ItemCount { get; set; }

            public Func<int> GetPriceFunc { get; }

            public LineItem(string item, Func<int, int> getPriceFunc)
            {
                GetPriceFunc = () => getPriceFunc(ItemCount);
                Item = item;
            }
        }
    }
}
