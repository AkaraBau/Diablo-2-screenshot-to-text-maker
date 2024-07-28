using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class Item : IComparable<Item>
    {
        public ItemType name { get; set; }
        public string type { get; set; }
        public string defense { get; set; }
        public int defenseAmount { get; set; }
        public string req1 { get; set; }
        public int level { get; set; }
        public List<Stats> ListOfStats = new List<Stats>();

        public Item(string[] data)
        {
            name = ItemTypeLookup.GetTypeFromDictionary(data[1]); 
            type = data[1];
            defenseAmount = Utils.ExtractIntFromString(data[2]);
            defense = Utils.RemoveNumbers(data[2]);
            level = Utils.ExtractIntFromString(data[6]);
            req1 = Utils.RemoveNumbers(data[6]);


            for (int i = 7; i < data.Length; i++)
            {
                Stats stats = new Stats(data[i]);
                ListOfStats.Add(stats);
            }
        }
        public int CompareTo(Item other, Stats other1) 
        {
          Stats.CompareTo(other1); 
          if (this.name == other.name)
          {
            return +1;
          }
          else 
          {
            return -1;
          }
        }
        public override string ToString()
        {
            string result = $"{name}/{type}/{defenseAmount}{defense}/{level}{req1}\t";
            var StringOfStats = Utils.StatsToString(ListOfStats);

            for (int i = 0; i < StringOfStats.Count; i++)
            {
                if (i >= 1 && i < StringOfStats.Count)
                {
                    result += "/";
                }
                result += $"{StringOfStats[i]}";
            }

            return result;
        }
    }
}