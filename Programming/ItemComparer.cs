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

namespace Programming
{
  class SortByStat : IComparer<Item>
  {
    public string sortCall { get; set; }

    public SortByStat(string sortInput)
    {
      sortCall = sortInput;
    }

    public int Compare(Item left, Item right)
    {
      Stats statsLeft = left.GetStat(sortCall);
      Stats statsRight = right.GetStat(sortCall);
      
      return Utils.compareStat(statsLeft, statsRight);
    }
  }
  public class GenericBeltSort : IComparer<Item>
  {
    public string[] SortParameters { get; set; }

    public GenericBeltSort(string[] inputParameters)
    {
    SortParameters = inputParameters; 
    }

    public int Compare(Item left, Item right)
    {
      return Utils.compareMultipleStats(left, right, SortParameters);
    }

  }
}