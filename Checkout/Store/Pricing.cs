using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Checkout.Store
{
    public class Pricing
        : IPricing
    {
        public ReadOnlyCollection<ISku> AvailableSkus { get; }

        public Pricing(params ISku[] skus)
        {
            if (skus.Any(s => s == null))
            {
                throw new ArgumentException("Invalid sku item in the sku list", "skus");
            }

            AvailableSkus = skus.ToList().AsReadOnly();
        }

        public int GetPrice(int itemCount)
        {
            throw new System.NotImplementedException();
        }
    }
}
