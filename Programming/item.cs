using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace DiabloItemMuleSystem
{
    public class Item
    {
        private static int itemIDseed = 1;
        public int Id { get; }
        public ItemType Name { get; set; }
        public string LevelRequirement { get; set; }
        public int Level { get; set; }
        public List<Stats> ListOfStats = new List<Stats>();

        public Item(List<string> data)
        {
            Id = itemIDseed++;
            Name = ItemTypeLookup.GetTypeFromDictionary(data[0]);
            
            Level = Utils.ExtractIntFromString(data[1]);
            LevelRequirement = Utils.RemoveNumbers(data[1]);
            for (int i = 2; i < data.Count; i++)
            {
                if (!data[i].Contains("CHARGES") ||
                   !data[i].Contains("CTC") ||
                   !data[i].Contains("CDMG") ||
                   !data[i].Contains("ATDO") ||
                   !data[i].Contains("DPL")||
                   !data[i].Contains("MS") ||
                   !data[i].Contains("LIGHTRADIUS"))
                {
                    Stats stats = new Stats(data[i]);
                    ListOfStats.Add(stats);
                }
            }
        }
        /// <summary>
        /// GetStat and GetAmount instance methods on type Item 
        /// </summary>
        public Stats GetStat(string inputString)
        {
            Stats result = null;
            for (int i = 0; i < ListOfStats.Count; i++)
            {
                if (ListOfStats[i].Name.Contains(inputString))
                {
                    result = ListOfStats[i];
                }
            }
            return result;
        }
        public Stats GetAmount(int bot, int top, Stats inputStats)
        {
            Stats result = null; 
            if (inputStats.Amount > bot && inputStats.Amount < top) 
            {
                result = inputStats;
            }
            return result;
        }
        /// <summary>
        /// String override and equals/gethaschcode override 
        /// </summary>
        public override string ToString()
        {
            string result = $"{Id}/{Name}/{Level}{LevelRequirement}\t";
            var StringOfStats = Utils.StatsToString(ListOfStats);
            List<string> statNamesForPrint = new List<string>();
            string[] statNamesItems = new string[] { "FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "GOLD" };


            for (int i = 0; i < statNamesItems.Length; i++)
            {
                var stats = GetStat(statNamesItems[i]);
                if (stats != null)
                {
                    statNamesForPrint.Add(stats.ToString());
                }
            }
            for (int i = 0; i < statNamesForPrint.Count; i++)
            {
                if (i >= 1 && i < statNamesForPrint.Count)
                {
                    result += "/";
                }

                result += statNamesForPrint[i];
            }
            return result;
        }

        public override bool Equals(object obj)
        {
            Item other = obj as Item; 

            if (other == null) return false;    

            else if ( this.Name == other.Name 
                && this.LevelRequirement == other.LevelRequirement 
                && this.Level == other.Level
                && Utils.CheckEqualStats(this.ListOfStats,other.ListOfStats) ) 
            { 
                return true; 
            }
            else 
            { 
                return false; 
            } 
        }

        public override int GetHashCode()
        {
            return this.LevelRequirement.GetHashCode() ^ this.Level.GetHashCode() ^ this.ListOfStats.GetHashCode();
        }
    }
}