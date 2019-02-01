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
        }

        public int GetTotalPrice() => _lineItems.Sum(lineItem => lineItem.GetPriceFunc());

        public void Scan(string item)
        {
            var lineItems = new Dictionary<string, int>();
            

            if (lineItems.ContainsKey(item))
            {
                lineItems[item] += 1;
            }
            else
            {
                if (!_pricing.TryGetSku(item, out ISku sku))
                {
                    throw new ArgumentException("Invalid item", "item");
                }
                
                _lineItems.Add(new LineItem(itemCount => sku.GetPrice(new ItemCount(itemCount))));
            }
        }

        private class LineItem
        {
            public string Item { get; set; }
            public int Count { get; set; }

            public Func<int> GetPriceFunc { get; }

            public LineItem(Func<int, int> getPriceFunc)
            {
                GetPriceFunc = () => getPriceFunc(Count);
            }
        }
    }
}
