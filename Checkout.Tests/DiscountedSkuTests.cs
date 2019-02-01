using Checkout.Store;
using Moq;
using NUnit.Framework;
using System;

namespace Checkout.Tests
{
    [TestFixture]
    public class DiscountedSkuTests
    {
        [Test]
        public void NullSku_ThrowException()
        {
            Assert.Throws<ArgumentException>(() => new DiscountedSku(null, 3, 100));
        }

        [Test]
        [TestCase(0, 100, 10)]
        [TestCase(3, 0, -5)]
        [TestCase(-1, 100, 10)]
        [TestCase(3, -5, -7)]
        [TestCase(3, 100, 101)]
        public void InvalidInputs_ThrowException(int specialPriceItemCount, int specialPrice, int unitPrice)
        {
            var skuMock = new Mock<IDiscountableSku>();
            skuMock.Setup(x => x.Name).Returns('A');
            skuMock.Setup(x => x.UnitPrice).Returns(unitPrice);

            Assert.Throws<ArgumentException>(() => new DiscountedSku(skuMock.Object, specialPriceItemCount, specialPrice));
        }

        [Test]
        [TestCase('A', 50, 3, 130, 1, ExpectedResult = 50)]
        [TestCase('A', 50, 3, 130, 2, ExpectedResult = 100)]
        [TestCase('A', 50, 3, 130, 3, ExpectedResult = 130)]
        [TestCase('A', 50, 3, 130, 4, ExpectedResult = 180)]
        [TestCase('A', 50, 3, 130, 6, ExpectedResult = 260)]
        [TestCase('B', 30, 2, 45, 1, ExpectedResult = 30)]
        [TestCase('B', 30, 2, 45, 2, ExpectedResult = 45)]
        [TestCase('B', 30, 2, 45, 3, ExpectedResult = 75)]
        [TestCase('B', 30, 2, 45, 4, ExpectedResult = 90)]
        [TestCase('B', 30, 2, 45, 5, ExpectedResult = 120)]
        [TestCase('B', 30, 2, 45, 6, ExpectedResult = 135)]
        public int ValidTests(
            char name, 
            int unitPrice, 
            int specialPriceItemCount, 
            int specialPrice, 
            int itemCount)
        {
            var itemCountMock = new Mock<IItemCount>();
            itemCountMock.Setup(x => x.Value).Returns(itemCount);

            var skuMock = new Mock<IDiscountableSku>();
            skuMock.Setup(x => x.Name).Returns(name);
            skuMock.Setup(x => x.UnitPrice).Returns(unitPrice);
            skuMock.Setup(x => x.GetPrice(It.IsAny<IItemCount>())).Returns((itemCount % specialPriceItemCount) * unitPrice);
            var target = new DiscountedSku(skuMock.Object, specialPriceItemCount, specialPrice);

            return target.GetPrice(itemCountMock.Object);
        }
    }
}
