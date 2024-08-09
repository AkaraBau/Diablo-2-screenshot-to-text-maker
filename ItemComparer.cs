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
  class SortItemBy : IComparer<Item>
  {
    public string sortCall { get; set; }

    public SortItemBy(string input)
    {
      sortCall = input;
    }

    public int Compare(Item left, Item right)
    {
      return Utils.compareStat(left, right, sortCall);
    }
  }
  class GenericItemSort
  {

  }
}