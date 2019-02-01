using Checkout.Store;
using NUnit.Framework;
using System;

namespace Checkout.Tests
{
    [TestFixture]
    public class SkuTests
    {
        [Test]
        [TestCase(null, 100)]
        [TestCase('1', 100)]
        [TestCase('#', 50)]
        [TestCase('\0', 50)]
        [TestCase('A', 0)]
        [TestCase('A', -1)]
        public void InvalidInput_ThrowException(char name, int unitPrice)
        {
            Assert.Throws<ArgumentException>(() => new Sku(name, unitPrice));
        }

        [Test]
        [TestCase('C', 20, 2, ExpectedResult = 40)]
        [TestCase('D', 15, 1, ExpectedResult = 15)]
        [TestCase('C', 10, 5, ExpectedResult = 50)]
        [TestCase('D', 5, 5, ExpectedResult = 25)]
        public int ValidTest(char name, int unitPrice, int itemCount)
        {
            var target = new Sku(name, unitPrice);
            return target.GetPrice(new ItemCount(itemCount));
        }
    }
}
