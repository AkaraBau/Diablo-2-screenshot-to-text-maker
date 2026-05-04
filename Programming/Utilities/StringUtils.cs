using System.Text.RegularExpressions;

namespace DiabloItemMuleSystem.Utilities
{
    public class StringUtils
    {
        public static string ShortenString(string input) // j
        {
            //DPL
            string result = input.Replace("DEFENSEBASEDONCHARACTERLEVEL", "DPL")
                                 .Replace("DEFENSEBASEDONCHARACTERLEVEL", "DPL")
                                 .Replace("DERENSEBASEDONCHARACTERLEVEL", "DPL")
                                 //ED
                                 .Replace("ENHANCEDDEFENSE", "ED")
                                 //defense
                                 .Replace("DEFENSE:", "DEF")
                                 .Replace("DEFENSE", "DEF")
                                 .Replace("DERENSE", "DEF")
                                 .Replace("DERENSECT", "DEF")
                                 .Replace("QDEFBASEDSNCHARACTERLEVEL", "DPL")
                                 //req
                                 .Replace("REQUIREDLEVEL:", "LREQ")
                                 .Replace("REQUIREDLEVET:", "LREQ")
                                 .Replace("REQUIREDLEVEL", "LREQ")
                                 .Replace("REQUIREDLEVET", "LREQ")
                                 //base
                                 .Replace("VAMPIREFANGBELT", "VB")
                                 .Replace("VANPIREFANGBELT", "VB")
                                 .Replace("MANPIREFANGBELT", "VB")
                                 .Replace("SHARKSKINBELT", "SB")
                                 .Replace("SHARESKINBELT", "SB")
                                 .Replace("SPIDERWEBSASH", "SPS")
                                 .Replace("MITHRILCOIL", "MC")
                                 .Replace("DEMONHIDESASH", "DHS")
                                 //stats craft roll 
                                 .Replace("FASTERCASTRATE", "FCR")
                                 //mana
                                 .Replace("TOMANA", "MANA")
                                 .Replace("TOIMANA", "MANA")
                                 .Replace("TEMANA", "MANA")
                                 .Replace("T6MANA", "MANA")
                                 .Replace("T6IMANA", "MANA")
                                 .Replace("TEIMANA", "MANA")
                                 .Replace("TEINANA", "MANA")
                                 .Replace("IREMANA", "MANA")
                                 //reg
                                 .Replace("REGENERATEMANA", "MREG")
                                 .Replace("REGENERATEIMANA", "MREG")
                                 .Replace("REGENERATEIMIANA", "MREG")
                                 .Replace("REGENERAMANA", "MREG")
                                 .Replace("REGEMERAMANA", "MREG")
                                 .Replace("REGENERATEMAMA", "MREG")
                                 //PLR
                                 .Replace("POISONLENGTHREDUCEDBY", "PLR")
                                 .Replace("POISONLENGTH", "PLR")
                                 .Replace("PEISENLENGTHREDUCEDBY", "PLR")
                                 .Replace("POISENLENGTHREDUCEDBY", "PLR")
                                 .Replace("PEISENLENGTHREDUCEDSV", "PLR")
                                 //LR
                                 .Replace("LIGHTNINGRESIST", "LR")
                                 .Replace("LIGHTHINGRESIST", "LR")
                                 //FR
                                 .Replace("FIRERESIST", "FR")
                                 //PR
                                 .Replace("POISONRESIST", "PR")
                                 .Replace("POISENRESIST", "PR")
                                 .Replace("PEISENRESIST", "PR")
                                 .Replace("POISONROSIST", "PR")
                                 .Replace("PEISONRESIST", "PR")
                                 .Replace("PERSONRESIST", "PR")
                                 .Replace("PÆISENRESIST", "PR")
                                 .Replace("PERSENRESIST", "PR")
                                 .Replace("PE1ISONRESIST", "PR")
                                 //cold resist
                                 .Replace("COLDRESIST", "CR")
                                 .Replace("CORPRESIST", "CR")
                                 .Replace("CERLPRESIST", "CR")
                                 .Replace("CELDRESIST", "CR")
                                 .Replace("CELNRESIST", "CR")
                                 .Replace("CELPRESIST", "CR")
                                 .Replace("1CERPRESIST", "CR")
                                 .Replace("CERPRESIST", "CR")
                                 .Replace("CERPDRESIST", "CR")
                                 .Replace("COLDR&SIST", "CR")
                                 .Replace("COLORESIST", "CR")
                                 .Replace("COLPRESIST", "CR")
                                 //all res 
                                 .Replace("ALLRESISTANCES", "@res")
                                 .Replace("ALLRESISTANCES", "@res")
                                 .Replace("AELRESISTANCES", "@res")
                                 // dex 
                                 .Replace("TEDEXTERITY", "DEX")
                                 .Replace("TODEXTERITY", "DEX")
                                 //str
                                 .Replace("TOSTRENGTH", "STR")
                                 .Replace("TESTRENGTH", "STR")
                                 .Replace("TOOSTRENGTH", "STR")
                                 .Replace("7OSTRENGTH", "STR")
                                 //life
                                 .Replace("TOLIFO", "LIFE")
                                 .Replace("TOLIFE", "LIFE")
                                 .Replace("JLIFE", "LIFE")
                                 .Replace("T0OLIFE", "LIFE")
                                 .Replace("TELIFO", "LIFE")
                                 .Replace("TELIFE", "LIFE")
                                 .Replace("TAELIFE", "LIFE")
                                 .Replace("TELIRE", "LIFE")
                                 .Replace("10YLIFE", "LIFE")
                                 .Replace("TOLRFE", "LIFE")
                                 .Replace("10LIFE", "LIFE")
                                 //rep 
                                 .Replace("REPLENISHLIFE", "REP")
                                 .Replace("REPLENISHLIFO ", "REP")
                                 //fhr
                                 .Replace("FASTERHITRECOVERY", "FHR")
                                 .Replace("FASTERHITRECOVER", "FHR")
                                 .Replace("FASTERHIFRECOVERY", "FHR")
                                 .Replace("FASTERHITRECEVERY", "FHR")
                                 //EG
                                 .Replace("EXTRAGOLDFROMMONSTERS", "GOLD")
                                 .Replace("EXTRAGOLDFROMMONSTERS", "GOLD")
                                 //ATDO
                                 .Replace("ATTACKERTAKESDAMAGEOF", "ATDO")
                                 //Light radius
                                 .Replace("TELIGHTRADIUS", "LIGHTRADIUS")
                                 .Replace("TOLIGHTRADIUS", "LIGHTRADIUS")
                                 //Ctc ws
                                 .Replace("CHANCETOCASTLEVEL", "CTC")
                                 .Replace("WHENSTRUCK", "")
                                 .Replace("NOVA", "")
                                 //stamina 
                                 .Replace("MAXIMUMSTAMINA", "MS")
                                 .Replace("ADDS", "")
                                 .Replace("COLDDAMAGE", "CDMG")
                                 .Replace("DAMAGEREDUCEDBY", "DR")
                                 .Replace("DAMACEREDUCEDBY", "DR")
                                 .Replace("LEAS", "7%")
                                 .Replace("REPAIRSDURABILITYIN33SECONDS", "")
                                 .Replace("N0", "26%")
                                 //attack raiting 
                                 .Replace("TOATTACKRATING", "AR")
                                 .Replace("TEATTACKRATING", "AR")
                                 .Replace("BONUSAR", "%AR")
                                 .Replace("BENUSAR", "%AR")
                                 //damage
                                 .Replace("TEMINIMUMDAMAGE", "MIN")
                                 .Replace("TEMAXIMUMDAMAGE", "MAX")
                                 //mf
                                 .Replace("BETTERCHANCEOFGETTINGMAGICITEMS", "MF")
                                 .Replace("BETTERCHANCEOFGETTINGMACICITEMS", "MF")
                                 .Replace("BETTERCHANCEOFGETTINGMAAGICITEMS", "MF")
                                 .Replace("BETTERCHANCEOFGETTINGMACGICITEMS", "MF")
                                 //ll ml 
                                 .Replace("LIFESTOLENPERHIT", "LL")
                                 .Replace("MANAAFTEREACHKILL", "MAEK")
                                 .Replace("MANASTOLENPERHIT", "ML")
                                 .Replace("INANASTOLENPERHIT", "ML")
                                 //charges 
                                 .Replace("LEVELCHARGEDBOLTCHARGES", "CHARGES")
                                 //other
                                 .Replace("MACICDR", "MDR")
                                 .Replace("MAACICDAMACEREPUCEBY", "MDR")
                                 .Replace("QDPL", "DPL");

            return result;
        }
        public static string RemoveAllWhiteSpace(string input)
        {
            return Regex.Replace(input, @"[ \t\r\f\v]", "");
        }
        public static string ChangeLetters(string input)
        {
            string result = input.ToUpper()
                                 .Replace("1@", "10")
                                 .Replace("1O", "10")
                                 .Replace("10O", "100")
                                 .Replace("1O0", "100")
                                 .Replace('®', 'O')
                                 .Replace('@', 'O')
                                 .Replace('©', 'O')
                                 .Replace('£', 'E')
                                 .Replace("RR", "R")
                                 .Replace("PP", "P")
                                 .Replace("SS", "S")
                                 .Replace("NN", "N")
                                 .Replace("II", "I")
                                 .Replace("EE", "E")
                                 .Replace("É", "E")
                                 .Replace("[", "")
                                 .Replace("#", "")
                                 .Replace("+", "")
                                 .Replace("@", "")
                                 .Replace("|", "")
                                 .Replace("*", "")
                                 .Replace("(", "")
                                 .Replace(")", "")
                                 .Replace(".", "")
                                 .Replace(";", "")
                                 .Replace("}", "")
                                 .Replace("/", "")
                                 .Replace(",", "")
                                 .Replace("]", "")
                                 .Replace("'", "")
                                 .Replace(":", "")
                                 .Replace("%", "")
                                 .Replace("«", "")
                                 .Replace("-", "")
                                 .Replace("“", "")
                                 .Replace("<", "")
                                 .Replace(">", "")
                                 .Replace("-", "")
                                 .Replace("—", "")
                                 .Replace("=", "")
                                 .Replace("°", "");
            return result;
        }
        public static int ExtractInt(string input)
        {
            string pattern = @"\d+";
            string combinedNumbers = "";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                combinedNumbers += match.Value;
            }
            int.TryParse(combinedNumbers, out int result);

            return result;

        }
        public static string RemoveNumbers(string input)
        {
            string pattern = @"\d";
            string result = Regex.Replace(input, pattern, "");
            return result;
        }
    }
}
