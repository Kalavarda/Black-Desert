﻿using System.Diagnostics;

namespace Barter.Model
{
    public class TradeItem
    {
        public string Name { get; set; }

        public TradeItemLevel Level { get; set; }

        public string ImageAddress { get; set; }

        public override string ToString()
        {
            return $"[{(int)Level} ур.] {Name}";
        }
    }

    public enum TradeItemLevel
    {
        Level_0,
        Level_1,
        Level_2,
        Level_3,
        Level_4,
        Level_5,
    }
}
