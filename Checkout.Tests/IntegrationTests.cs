using NUnit.Framework;
using Checkout.Store;
using store = Checkout.Store;
namespace Checkout.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        [TestCase(1, 1, 1, 1, ExpectedResult = 115)]
        [TestCase(1, 2, 1, 1, ExpectedResult = 130)]
        [TestCase(2, 2, 2, 2, ExpectedResult = 215)]
        [TestCase(3, 3, 3, 3, ExpectedResult = 310)]
        [TestCase(1, 2, 3, 4, ExpectedResult = 215)]
        [TestCase(3, 2, 1, 1, ExpectedResult = 210)]
        [TestCase(3, 2, 10, 20, ExpectedResult = 675)]
        public int Tests(int aItemCount = 0,
            int bItemCount = 0,
            int cItemCount = 0,
            int dItemCount = 0)
        {
            var aSku = new DiscountedSku(new Sku('A', 50), 3, 130);
            var bSku = new DiscountedSku(new Sku('B', 30), 2, 45);
            var cSku = new Sku('C', 20);
            var dSku = new Sku('D', 15);

            var pricing = new Pricing(aSku, bSku, cSku, dSku);
            var checkout = new store.Checkout(pricing);

            void callScan(string item, int itemCount)
            {
                var counter = itemCount;
                while (counter != 0)
                {
                    checkout.Scan(item);
                    counter--;
                }
            }

            callScan("A", aItemCount);
            callScan("B", bItemCount);
            callScan("C", cItemCount);
            callScan("D", dItemCount);

            return checkout.GetTotalPrice();
        }
    }
}
