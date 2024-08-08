using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class Item
    {
        public ItemType name { get; set; }
        public string type { get; set; }
        public string defense { get; set; }
        public int? defenseAmount { get; set; }
        public string req1 { get; set; }
        public int level { get; set; }
        public List<Stats> ListOfStats = new List<Stats>();

        public Item(string[] data)
        {
            name = ItemTypeLookup.GetTypeFromDictionary(data[1]);
            type = data[1];
            if (type == "RING" || type == "AMULET")
            {
                defenseAmount = null;
                defense = null;
                level = Utils.ExtractIntFromString(data[2]);
                req1 = Utils.RemoveNumbers(data[2]);
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
            else if (type == "JEWEL")
            {
                defenseAmount = null;
                defense = null;
                level = Utils.ExtractIntFromString(data[3]);
                req1 = Utils.RemoveNumbers(data[3]);
                for (int i = 4; i < data.Length; i++)
                {
                    Stats stats = new Stats(data[i]);
                    ListOfStats.Add(stats);
                }
            }
            else
            {
                defenseAmount = Utils.ExtractIntFromString(data[2]);
                defense = Utils.RemoveNumbers(data[2]);
                level = Utils.ExtractIntFromString(data[6]);
                req1 = Utils.RemoveNumbers(data[6]);


                for (int i = 7; i < data.Length; i++)
                {
                    if (data[i].Contains("CHARGES") ||
                       data[i].Contains("CTC") ||
                       data[i].Contains("CDMG") ||
                       data[i].Contains("ATDO") == false)
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
                if (ListOfStats[i].name.Contains(inputString))
                {
                    result = ListOfStats[i];
                }
            }
            return result;
        }
        public override string ToString()
        {
            string result = $"{name}/{type}/{defenseAmount}{defense}/{level}{req1}\t";
            var StringOfStats = Utils.StatsToString(ListOfStats);

            if (type == "RING" || type == "AMULET" || type == "JEWEL")
            {
                result = $"{name}/{level}{req1}\t";
            }
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