namespace Checkout.Store
{
    public class Pricing
        : IPricing
    {

        public Pricing(params ISku[] skus)
        {

        }

        public int GetPrice(int itemCount)
        {
            throw new System.NotImplementedException();
        }
    }
}
