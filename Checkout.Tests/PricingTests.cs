using Checkout.Store;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.Tests
{
    [TestFixture]
    public class PricingTests
    {
        [Test]
        public void NullSku_ThrowException()
        {
            var skuMock = new Mock<ISku>();
            skuMock.Setup(x => x.Name).Returns('A');
            skuMock.Setup(x => x.GetPrice(It.IsAny<IItemCount>())).Returns(1);

            var skus = new[] { null, skuMock.Object };

            Assert.Throws<ArgumentException>(() => new Pricing(skus));
        }

        [Test]
        public void DuplicateSku_ThrowException()
        {
            var skuMock = new Mock<ISku>();
            skuMock.Setup(x => x.Name).Returns('A');
            skuMock.Setup(x => x.GetPrice(It.IsAny<IItemCount>())).Returns(1);

            var skus = new[] { skuMock.Object, skuMock.Object };

            Assert.Throws<ArgumentException>(() => new Pricing(skus));
        }

        [Test]
        public void ValidSkus()
        {
            var aSkuMock = new Mock<ISku>();
            aSkuMock.Setup(x => x.Name).Returns('A');
            aSkuMock.Setup(x => x.GetPrice(It.IsAny<IItemCount>())).Returns(1);

            var bSkuMock = new Mock<ISku>();
            bSkuMock.Setup(x => x.Name).Returns('B');
            bSkuMock.Setup(x => x.GetPrice(It.IsAny<IItemCount>())).Returns(1);

            var skus = new[] { aSkuMock.Object, bSkuMock.Object };

            var target = new Pricing(skus);

            Assert.True(target.AvailableSkus.Count == 2);
            Assert.True(target.AvailableSkus.SingleOrDefault(s => s.Name == 'A') != null);
            Assert.True(target.AvailableSkus.SingleOrDefault(s => s.Name == 'B') != null);

            Assert.True(target.TryGetSku("A", out ISku aSku));
            Assert.True(target.TryGetSku("B", out ISku bSku));
            Assert.False(target.TryGetSku("C", out ISku cSku));

            Assert.True(aSku.Name == 'A');
            Assert.True(bSku.Name == 'B');

        }
    }
}
