using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using System.Xml.XPath;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;
using System.Text.RegularExpressions;

namespace AkarasDegenStuff
{
    class ItemComparer : IComparer<Item>
    {
        public int Compare(Item left, Item right)
        {
            // First, compare the overall item names and types.
            if (left.type != right.type)
            {
                return string.Compare(left.type, right.type);
            }
            int shortestList = Math.Min(left.ListOfStats.Count, right.ListOfStats.Count); // get the lowest count out of both lists 
            for (int i = 0; i < shortestList; i++)
            {
                var leftStat = left.ListOfStats[i];
                var rightStat = right.ListOfStats[i];

                // Compare stats by name first to ensure correct stat matching
                int statNameComparison = string.Compare(leftStat.name, rightStat.name);
                if (statNameComparison != 0)
                {
                    return statNameComparison;
                }

                // If stat names are equal, compare their amounts
                if (leftStat.amount != rightStat.amount)
                {
                    return (int)leftStat.amount - (int)rightStat.amount;
                }
            }
              // If all compared stats are equal, compare by the number of stats
            return left.ListOfStats.Count.CompareTo(right.ListOfStats.Count);

        }
    }
}