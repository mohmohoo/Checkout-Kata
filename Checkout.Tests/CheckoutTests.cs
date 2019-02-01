using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using store = Checkout.Store;

namespace Checkout.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        delegate void TryGetSkuCallBack(string item, out ISku amount);

        [Test]
        public void NullPricing_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new store.Checkout(null));
        }

        [Test]
        public void ItemDoesNotExist_ThrowException()
        {
            var pricingMock = new Mock<IPricing>();
            pricingMock.Setup(x => x.TryGetSku(It.IsAny<string>(), out It.Ref<ISku>.IsAny)).Returns(false);
            var target = new store.Checkout(pricingMock.Object);
            Assert.Throws<ArgumentException>(() => target.Scan("A"));
        }

        [Test]
        [TestCase(1, 1, 1, 1, ExpectedResult = 115)]
        [TestCase(1, 2, 1, 1, ExpectedResult = 130)]
        [TestCase(2, 2, 2, 2, ExpectedResult = 215)]
        [TestCase(3, 3, 3, 3, ExpectedResult = 310)]
        [TestCase(1, 2, 3, 4, ExpectedResult = 215)]
        [TestCase(3, 2, 1, 1, ExpectedResult = 210)]
        public int Valid(
            int aItemCount = 0,
            int bItemCount = 0,
            int cItemCount = 0,
            int dItemCount = 0)
        {
            int getTotalForA()
                => ((aItemCount % 3) * 50) + ((aItemCount / 3) * 130);

            var aSkuMock = new Mock<ISku>();
            aSkuMock
                .Setup(x => x.Name)
                .Returns('A');
            aSkuMock
                .Setup(x => x.GetPrice(It.IsAny<IItemCount>()))
                .Returns(getTotalForA());

            int getTotalForB()
                => ((bItemCount % 2) * 30) + ((bItemCount / 2) * 45);

            var bSkuMock = new Mock<ISku>();
            bSkuMock
                .Setup(x => x.Name)
                .Returns('B');
            bSkuMock
                .Setup(x => x.GetPrice(It.IsAny<IItemCount>()))
                .Returns(getTotalForB());

            int getTotalForC()
                => cItemCount * 20;

            var cSkuMock = new Mock<ISku>();
            cSkuMock
                .Setup(x => x.Name)
                .Returns('C');
            cSkuMock
                .Setup(x => x.GetPrice(It.IsAny<IItemCount>()))
                .Returns(getTotalForC());

            int getTotalForD()
                => dItemCount * 15;
            var dSkuMock = new Mock<ISku>();
            dSkuMock
                .Setup(x => x.Name)
                .Returns('D');
            dSkuMock
                .Setup(x => x.GetPrice(It.IsAny<IItemCount>()))
                .Returns(getTotalForD());

            var availableSkus = new[]
            {
                aSkuMock.Object,
                bSkuMock.Object,
                cSkuMock.Object, 
                dSkuMock.Object

            }.ToList().AsReadOnly();

            
            
            var pricingMock = new Mock<IPricing>();
            pricingMock.Setup(x => x.AvailableSkus)
                .Returns(availableSkus);
            void SetupTryGetSku (string item, ISku sku)
            {
                pricingMock
                .Setup(x => x.TryGetSku(item, out It.Ref<ISku>.IsAny))
                .Callback(new TryGetSkuCallBack((string _item, out ISku _sku) =>
                {
                    _sku = sku;
                }))
                .Returns(true);
            }

            SetupTryGetSku("A", aSkuMock.Object);
            SetupTryGetSku("B", bSkuMock.Object);
            SetupTryGetSku("C", cSkuMock.Object);
            SetupTryGetSku("D", dSkuMock.Object);

            var target = new store.Checkout(pricingMock.Object);

            void callScan(string item, int itemCount)
            {
                var counter = itemCount;
                while (counter != 0)
                {
                    target.Scan(item);
                    counter--;
                }

            }

            callScan("A", aItemCount);
            callScan("B", bItemCount);
            callScan("C", cItemCount);
            callScan("D", dItemCount);

            return target.GetTotalPrice();
        }
    }
}
