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
  class SortByStat : IComparer<Item>
  {
    public string sortCall { get; set; }

    public SortByStat(string sortInput)
    {
      sortCall = sortInput;
    }

    public int Compare(Item left, Item right)
    {
      Stats statsLeft = left.getStat(sortCall);
      Stats statsRight = right.getStat(sortCall);
      
      return Utils.compareStat(statsLeft, statsRight);
    }
  }
  class GenericBeltSort : IComparer<Item>
  {
    public string[] sortParameters { get; set; }

    public GenericBeltSort(string[] inputParameters)
    {
    sortParameters = inputParameters; 
    }

    public int Compare(Item left, Item right)
    {
      return Utils.compareMultipleStats(left, right, sortParameters);
    }

  }
}