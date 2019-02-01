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
        public void InvalidInput_ThrowException(char name, int unitPrice)
        {
            Assert.Throws<ArgumentException>(() => new Sku(name, unitPrice));
        }
    }
}
