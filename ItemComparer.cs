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
            var shortestList = Math.Min(left.ListOfStats.Count, right.ListOfStats.Count); 

          for (int i = 0; i < shortestList; i++)
          {
            for (int j = 0; j < shortestList; j++)
            {
                if (left.ListOfStats[i].name.Contains("STR") && right.ListOfStats[j].name.Contains("STR"))
                {
                return (int)left.ListOfStats[i].amount - (int)right.ListOfStats[j].amount; 
                }
            }
          }
        return 0; 
        }
    }
}