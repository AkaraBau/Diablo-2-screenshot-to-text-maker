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
        Stats strA = left.getStat("STR");
        Stats strB = right.getStat("STR");

        if (strA == null)
        {
          return 1; 
        }
        else if (strB == null)
        {
          return -1; 
        }

        return strA.name.CompareTo(strB.name); 
        }
    }
}