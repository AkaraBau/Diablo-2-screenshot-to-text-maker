using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabloItemMuleSystem.Models
{
    public static class StatTypeLookup
    {
        private static readonly Dictionary<string, StatType> _ConvertStatName = new Dictionary<string, StatType>(StringComparer.OrdinalIgnoreCase)
        {
            {"faster cast rate" , StatType.FCR},
            {"fastercastrate" , StatType.FCR },
            {"fcr" , StatType.FCR },           
            {"faster hit recovery" , StatType.FHR },            
            {"fhr" , StatType.FHR },
            {"fasterhitrecovery" , StatType.FHR },
            {"strength" , StatType.STR },
            {"str" , StatType.STR },
            {"dex" , StatType.DEX },
            {"dexterity" , StatType.DEX },
            {"life leech" , StatType.LL },
            {"lifeleech" , StatType.LL },
            {"ll" , StatType.LL },
            {"vita" , StatType.VITA },
            {"ene" , StatType.ENERGY },
            {"energy" , StatType.ENERGY },
            {"manaleech" , StatType.ML },
            {"mana leech" , StatType.ML },
            {"life" , StatType.LIFE },
            {"rep" , StatType.REP },
            {"replenish" , StatType.REP },
            {"mana" , StatType.MANA },
            {"manaregen" , StatType.MREG },
            {"mana regen" , StatType.MREG },
            {"mreg" , StatType.MREG },
            {"mana regeneration" , StatType.MREG },
            {"manaregeneration" , StatType.MREG },
            {"pr" , StatType.PR },
            {"poison resist" , StatType.PR },
            {"poisonresist" , StatType.PR },
            {"lr" , StatType.LR },
            {"lightning resist" , StatType.LR },
            {"lightningresist" , StatType.LR },
            {"fr" , StatType.FR },
            {"fireresist" , StatType.FR },
            {"fire resist" , StatType.FR },
            {"plr" , StatType.PLR },
            {"poison length reduced" , StatType.PLR },
            {"poisonlengthreduced" , StatType.PLR },
            {"ed" , StatType.ED },
            {"Enhanced defense" , StatType.ED },
            {"enhanceddefense" , StatType.ED },
            {"gold" , StatType.GOLD },
            {"extra gold" , StatType.GOLD },
            {"extra gold find" , StatType.GOLD },
            {"extragold" , StatType.GOLD },
            {"eg" , StatType.GOLD },
            {"extragoldfind" , StatType.GOLD },

        };
        public static StatType GetStatType(string input)
        {
            if (_ConvertStatName.TryGetValue(input, out var output))
            {
                output = _ConvertStatName[input];
            }
            return output;
        }
    }
}
