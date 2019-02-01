using Moq;
using NUnit.Framework;
using System;
using store = Checkout.Store;

namespace Checkout.Tests
{
    [TestFixture]
    public class CheckoutTests
    {
        [Test]
        public void NullPricing_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new store.Checkout(null));
        }

        [Test]
        public void ItemDoesNotExist_ThrowException()
        {
            var pricingMock = new Mock<IPricing>();
            pricingMock.Setup(x => x.TryGetSku(It.IsAny<string>(), out It.Ref<ISku>.IsAny)).Returns(null);
            var target = new store.Checkout(pricingMock.Object);
            Assert.Throws<ArgumentException>(() => target.Scan("A"));
        }
    }
}
