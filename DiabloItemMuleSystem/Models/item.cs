using System;
using System.Collections.Generic;
using DiabloItemMuleSystem.Utilities;

namespace DiabloItemMuleSystem.Models
{
    public class Item
    {
        
        public  Guid Id {  get; set; }
        public readonly ItemType Name; 
        public readonly int Level;
        public readonly List<Stats> ListOfStats = new List<Stats>();

        public Item(List<string> data)
        {
            Id = Guid.NewGuid();
            Name = ItemTypeLookup.GetTypeFromDictionary(data[0]);
            Level = StringUtils.ExtractInt(data[1]);

            for (int i = 2; i < data.Count; i++)
            {
                var trimmedData = StringUtils.RemoveNumbers(data[i]);
                StatType stat = StatTypeLookup.GetStatType(trimmedData);

                if (stat != StatType.NULL)
                {
                    Stats stats = new Stats(Id, data[i], stat);
                    ListOfStats.Add(stats);
                }
            }
        }
    
        // Constructor for fetching data from database to object 
        public Item(Item item, List<Stats> listOfStats)
        {
            Id = item.Id;
            Name = item.Name;            
            Level = item.Level;
            ListOfStats = listOfStats;
        }
        // empty constructor for the ItemContext
        public Item()
        {

        }
        /// <summary>
        /// GetStat and GetAmount instance methods on type Item 
        /// </summary>
        public Stats GetStat(StatType inputStat)
        {
            Stats result = null;
            for (int i = 0; i < ListOfStats.Count; i++)
            {
                if (ListOfStats[i].Name == inputStat)
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
            string result = $"{Name}/{Level}LREQ\t";
            List<string> statNamesForPrint = new List<string>();
            var allStatTypes = Enum.GetValues<StatType>(); 


            for (int i = 0; i < allStatTypes.Length; i++)
            {
                var stats = GetStat(allStatTypes[i]);
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
                && this.Level == other.Level
                && StatComparerUtils.CheckIfEqual(this.ListOfStats,other.ListOfStats) ) 
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
            return this.Level.GetHashCode() ^ this.ListOfStats.GetHashCode();
        }
    }
}