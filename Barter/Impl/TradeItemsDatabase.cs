using System;
using System.Collections.Generic;
using Barter.Model;

namespace Barter.Impl
{
    public class TradeItemsDatabase: ITradeItemsDatabase
    {
        public IReadOnlyCollection<TradeItem> GetAllItems()
        {
            return new []
            {
                new TradeItem
                {
                    Name = "Старый сундук с золотыми монетами",
                    Level = TradeItemLevel.Level_4,
                    ImageAddress = "https://bddatabase.net/items/new_icon/03_etc/10_free_tradeitem/00800018.png",
                },
                new TradeItem
                {
                    Name = "Синий кварц",
                    Level = TradeItemLevel.Level_5,
                    ImageAddress = "https://bddatabase.net/items/new_icon/03_etc/10_free_tradeitem/00800019.png",
                },
            };
        }

        public int GetMass(TradeItemLevel level)
        {
            switch (level)
            {
                case TradeItemLevel.Level_1:
                case TradeItemLevel.Level_2:
                    return 800;
                case TradeItemLevel.Level_3:
                    return 900;
                case TradeItemLevel.Level_4:
                case TradeItemLevel.Level_5:
                    return 1000;
                default:
                    throw new NotImplementedException();
            }
        }

        public int GetCost(TradeItemLevel level)
        {
            switch (level)
            {
                case TradeItemLevel.Level_1:
                case TradeItemLevel.Level_2:
                    return 0;
                case TradeItemLevel.Level_3:
                    return 1000000;
                case TradeItemLevel.Level_4:
                    return 2000000;
                case TradeItemLevel.Level_5:
                    return 5000000;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
