using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Barter.Model;
using Newtonsoft.Json;

namespace Barter.Impl
{
    public class TradeItemsDatabase: ITradeItemsDatabase
    {
        private ItemsData _data;

/*
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
*/
        private TradeItem[] _all;

        public TradeItemsDatabase()
        {
            var fileName = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            fileName = Path.Combine(fileName, "data.json");

            if (File.Exists(fileName))
                _data = ItemsData.Load(fileName);
            else
            {
                _data = new ItemsData
                {
                    Level_1 = _data.Level_1,
                    Level_2 = _data.Level_2,
                    Level_3 = _data.Level_3,
                    Level_4 = _data.Level_4,
                    Level_5 = _data.Level_5
                };
                _data.Save(fileName);
            }
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

        public IReadOnlyCollection<TradeItem> GetAllItems(TradeItemLevel? level = null)
        {
            if (level == null)
                return _all ??= new TradeItem[0]
                    .Union(_data.Level_1)
                    .Union(_data.Level_2)
                    .Union(_data.Level_3)
                    .Union(_data.Level_4)
                    .Union(_data.Level_5)
                    .ToArray();

            switch (level.Value)
            {
                case TradeItemLevel.Level_1:
                    return _data.Level_1;
                case TradeItemLevel.Level_2:
                    return _data.Level_2;
                case TradeItemLevel.Level_3:
                    return _data.Level_3;
                case TradeItemLevel.Level_4:
                    return _data.Level_4;
                case TradeItemLevel.Level_5:
                    return _data.Level_5;
                default:
                    throw new NotImplementedException();
            }
        }
    }

    public class ItemsData
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented
        };
        private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(SerializerSettings);

        public TradeItem[] Level_1 { get; set; }

        public TradeItem[] Level_2 { get; set; }
        
        public TradeItem[] Level_3 { get; set; }
        
        public TradeItem[] Level_4 { get; set; }
        
        public TradeItem[] Level_5 { get; set; }

        public void Save(string fileName)
        {
            using var file = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
            using var writer = new StreamWriter(file);
            JsonSerializer.Serialize(writer, this);
        }

        public static ItemsData Load(string fileName)
        {
            using var file = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            using var reader = new StreamReader(file);
            using var jsonReader = new JsonTextReader(reader);
            return JsonSerializer.Deserialize<ItemsData>(jsonReader);
        }
    }
}
