using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout.Store
{
    public class Checkout
        : ICheckout
    {
        private readonly IPricing _pricing;
        private readonly Dictionary<string, Func<int, int>> _pricings;
        private readonly Dictionary<string, int> _lineItems;

        public Checkout(IPricing pricing)
        {
            _pricing = pricing ?? throw new ArgumentException("Invalid null value", "pricing");

            _pricings = new Dictionary<string, Func<int, int>>();
            _lineItems = new Dictionary<string, int>();
        }

        public int GetTotalPrice() => _lineItems.Sum(lineItem => _pricings[lineItem.Key](lineItem.Value));

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

                _pricings.Add(item, itemCount => sku.GetPrice(new ItemCount(itemCount)));
            }
        }
    }
}
