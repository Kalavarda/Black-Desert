using Barter.Model;
using Moq;
using NUnit.Framework;

namespace Barter.Tests
{
    [TestFixture]
    public class Exchange_Test
    {
        private readonly Mock<ITradeItemsDatabase> _itemsDatabase = new Mock<ITradeItemsDatabase>();

        public Exchange_Test()
        {
            _itemsDatabase
                .Setup(db => db.GetMass(TradeItemLevel.Level_4))
                .Returns(1000);
            _itemsDatabase
                .Setup(db => db.GetMass(TradeItemLevel.Level_5))
                .Returns(1000);
        }

        [TestCase(1, 4, 4000)]
        [TestCase(3, 4, 4000)]
        public void GetSourceMass_Test(int ratio, int count, int expected)
        {
            var exchange = new Exchange
            {
                SourceItem = new TradeItem { Level = TradeItemLevel.Level_4 },
                Ratio = ratio,
                DestItem = new TradeItem { Level = TradeItemLevel.Level_5 },
                Count = count
            };

            Assert.AreEqual(expected, exchange.GetSourceMass(_itemsDatabase.Object));
        }

        [TestCase(1, 4, 4000)]
        [TestCase(3, 4, 12000)]
        public void GetDestMass_Test(int ratio, int count, int expected)
        {

            var exchange = new Exchange
            {
                SourceItem = new TradeItem { Level = TradeItemLevel.Level_4 },
                Ratio = ratio,
                DestItem = new TradeItem { Level = TradeItemLevel.Level_5 },
                Count = count
            };

            Assert.AreEqual(expected, exchange.GetDestMass(_itemsDatabase.Object));
        }
    }
}
