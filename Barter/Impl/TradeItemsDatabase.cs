using System;
using System.Collections.Generic;
using System.Linq;
using Barter.Model;

namespace Barter.Impl
{
    public class TradeItemsDatabase: ITradeItemsDatabase
    {
        private IReadOnlyCollection<TradeItem> Level_1_Items = new[]
        {
            new TradeItem
            {
                Name = "Морской боевой паек",
                Level = TradeItemLevel.Level_1
            },
        };

        private IReadOnlyCollection<TradeItem> Level_2_Items = new[]
        {
            new TradeItem
            {
                Name = "Разноцветные бусы",
                Level = TradeItemLevel.Level_2
            },
        };

        private IReadOnlyCollection<TradeItem> Level_3_Items = new []
        {
            new TradeItem
            {
                Name = "Древние песочные часы",
                Level = TradeItemLevel.Level_3
            },
        };

        private IReadOnlyCollection<TradeItem> Level_4_Items = new []
        {
            new TradeItem
            {
                Name = "Старый сундук с золотыми монетами",
                Level = TradeItemLevel.Level_4,
                ImageAddress = "https://bddatabase.net/items/new_icon/03_etc/10_free_tradeitem/00800018.png",
            },
            new TradeItem
            {
                Name = "Осколок аметиста",
                Level = TradeItemLevel.Level_4
            },
        };

        private IReadOnlyCollection<TradeItem> Level_5_Items = new []
        {
            new TradeItem
            {
                Name = "Синий кварц",
                Level = TradeItemLevel.Level_5,
                ImageAddress = "https://bddatabase.net/items/new_icon/03_etc/10_free_tradeitem/00800019.png",
            },
            new TradeItem
            {
                Name = "Восьмигранный сундук",
                Level = TradeItemLevel.Level_5
            },
        };

        private TradeItem[] _all;

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

        public IReadOnlyCollection<TradeItem> GetAllItems(TradeItemLevel? level = null)
        {
            if (level == null)
                return _all ??= new TradeItem[0]
                    .Union(Level_1_Items)
                    .Union(Level_2_Items)
                    .Union(Level_3_Items)
                    .Union(Level_4_Items)
                    .Union(Level_5_Items)
                    .ToArray();

            switch (level.Value)
            {
                case TradeItemLevel.Level_1:
                    return Level_1_Items;
                case TradeItemLevel.Level_2:
                    return Level_2_Items;
                case TradeItemLevel.Level_3:
                    return Level_3_Items;
                case TradeItemLevel.Level_4:
                    return Level_4_Items;
                case TradeItemLevel.Level_5:
                    return Level_5_Items;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
