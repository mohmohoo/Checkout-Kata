using System;

namespace Checkout.Store
{
    public class Checkout
        : ICheckout
    {
        private readonly IPricing _pricing;

        public Checkout(IPricing pricing)
        {
            _pricing = pricing;
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string item)
        {
            throw new NotImplementedException();
        }
    }
}
