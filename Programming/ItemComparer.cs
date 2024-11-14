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

namespace DiabloItemMuleSystem
{
  class SortByStat : IComparer<Item>
  {
    public string SortParameter { get; set; }

    public SortByStat(string sortParameter)
    {
      SortParameter = sortParameter;
    }

    public int Compare(Item left, Item right)
    {
      Stats statsLeft = left.GetStat(SortParameter);
      Stats statsRight = right.GetStat(SortParameter);
      
      return Utils.CompareStat(statsLeft, statsRight);
    }
  }
  public class GenericItemSort : IComparer<Item>
  {
    public string[] SortParameters { get; set; }

    public GenericItemSort(string[] sortParameters)
    {
    SortParameters = sortParameters; 
    }

    public int Compare(Item left, Item right)
    {
      return Utils.CompareMultipleStats(left, right, SortParameters);
    }

  }
}