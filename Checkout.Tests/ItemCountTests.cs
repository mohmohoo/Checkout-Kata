using Checkout.Store;
using NUnit.Framework;
using System;

namespace Checkout.Tests
{
    [TestFixture]
    public class ItemCountTests
    {
        [Test]
        [TestCase(-1)]
        [TestCase(-2)]
        [TestCase(0)]
        public void InvalidTests_ThrowException(int value)
        {
            Assert.Throws<ArgumentException>(() => new ItemCount(value));
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ValidTests(int value)
        {
            var target = new ItemCount(value);
            Assert.AreEqual(target.Value, value);
        }
    }
}
