using System.Linq;
using Barter.Impl;
using Barter.Model;
using Moq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace Barter.Tests
{
    [TestFixture]
    public class SearchEngine_Test
    {
        private readonly Mock<ITradeItemsDatabase> _itemsDatabase = new Mock<ITradeItemsDatabase>();
        
        private static readonly TradeItem[] _testItems = {
            new TradeItem
            {
                Name = "Осколок аметиста",
                Level = TradeItemLevel.Level_4
            },
            new TradeItem
            {
                Name = "Синий кварц",
                Level = TradeItemLevel.Level_5
            },
        };

        private readonly Mock<ITranslateService> _translateService = new Mock<ITranslateService>();

        [Test]
        public void SearchImpl_Test()
        {
            _itemsDatabase
                .Setup(db => db.GetAllItems(TradeItemLevel.Level_4))
                .Returns(_testItems.Where(i => i.Level == TradeItemLevel.Level_4).ToArray);

            var searchEngine = new SearchEngine(_itemsDatabase.Object, _translateService.Object);
            var result = searchEngine.Search("  сколо  метист  ", TradeItemLevel.Level_4);
            Assert.AreEqual("Осколок аметиста", result.Single().Name);
        }
    }
}
