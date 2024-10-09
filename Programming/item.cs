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

namespace Programming
{
    public class Item
    {
        public int itemID = 0; 
        public ItemType Name { get; set; }
        public string Type { get; set; }
        public string Defense { get; set; }
        public int? DefenseAmount { get; set; }
        public string LevelRequirement { get; set; }
        public int Level { get; set; }
        public List<Stats> ListOfStats = new List<Stats>();

        public Item(string[] data)
        {
            itemID++;
            
            Name = ItemTypeLookup.GetTypeFromDictionary(data[1]);
            Type = data[1];
            if (Type == "RING" || Type == "AMULET")
            {
                DefenseAmount = null;
                Defense = null;
                Level = Utils.ExtractIntFromString(data[2]);
                LevelRequirement = Utils.RemoveNumbers(data[2]);
                for (int i = 3; i < data.Length; i++)
                {
                    if (data[i].Contains("CHARGES") ||
                        data[i].Contains("CTC") ||
                        data[i].Contains("CDMG") == false)
                    {
                        Stats stats = new Stats(data[i]);
                        ListOfStats.Add(stats);
                    }
                }
            }
            else if (Type == "JEWEL")
            {
                DefenseAmount = null;
                Defense = null;
                Level = Utils.ExtractIntFromString(data[3]);
                LevelRequirement = Utils.RemoveNumbers(data[3]);
                for (int i = 4; i < data.Length; i++)
                {
                    Stats stats = new Stats(data[i]);
                    ListOfStats.Add(stats);
                }
            }
            else
            {
                DefenseAmount = Utils.ExtractIntFromString(data[2]);
                Defense = Utils.RemoveNumbers(data[2]);
                Level = Utils.ExtractIntFromString(data[6]);
                LevelRequirement = Utils.RemoveNumbers(data[6]);


                for (int i = 7; i < data.Length; i++)
                {
                    if (data[i].Contains("CHARGES") ||
                       data[i].Contains("CTC") ||
                       data[i].Contains("CDMG") ||
                       data[i].Contains("ATDO") ||
                       data[i].Contains("DPL") == false)
                    {
                        Stats stats = new Stats(data[i]);
                        ListOfStats.Add(stats);
                    }
                }
            }
        }
        public Stats getStat(string inputString)
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
        public override string ToString()
        {
            string result = $"{itemID}/{Name}/{Type}/{DefenseAmount}{Defense}/{Level}{LevelRequirement}\t";
            var StringOfStats = Utils.StatsToString(ListOfStats);
            List<string> statNamesForPrint = new List<string>();
            string[] statNamesBelt = new string[] { "FCR", "FHR", "STR", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "DPL", "QDPL", "LIGHTRADIUS", "MS", "ATDO", "GOLD" };

            if (Type == "RING" || Type == "AMULET" || Type == "JEWEL")
            {
                result = $"{Name}/{Level}{LevelRequirement}\t";
                for (int i = 0; i < StringOfStats.Count; i++)
                {
                    if (i >= 1 && i < StringOfStats.Count)
                    {
                        result += "/";
                    }

                    result += $"{StringOfStats[i]}";
                }
            }
            else if (Type == "SB" || Type == "VB")
            {
                for (int i = 0; i < statNamesBelt.Length; i++)
                {
                    var stats = getStat(statNamesBelt[i]);
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
            }
            else
            {
                for (int i = 0; i < StringOfStats.Count; i++)
                {
                    if (i >= 1 && i < StringOfStats.Count)
                    {
                        result += "/";
                    }

                    result += $"{StringOfStats[i]}";
                }
            }


            return result;
        }

        public override bool Equals(object obj)
        {
            Item other = obj as Item; 

            if (other == null) return false;    

            else if (   this.itemID == other.itemID 
                && this.Name == other.Name 
                && this.Type == other.Type 
                && this.Defense == other.Defense 
                && this.DefenseAmount == other.DefenseAmount 
                && this.LevelRequirement == other.LevelRequirement 
                && this.Level == other.Level
                && this.ListOfStats.Equals(other.ListOfStats)) 
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
            return this.itemID.GetHashCode() ^ this.Name.GetHashCode() ^ this.Type.GetHashCode() ^ this.Defense.GetHashCode() ^ this.DefenseAmount.GetHashCode() ^ this.LevelRequirement.GetHashCode() ^ this.Level.GetHashCode() ^ this.ListOfStats.GetHashCode();
        }
    }
}