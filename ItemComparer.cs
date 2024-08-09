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
      return Utils.compareStat(left, right, sortCall);
    }
  }

  /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

  class GenericBeltSort : IComparer<Item>
  {
    public List<string> beltSortCalls { get; set; }

    public GenericBeltSort(List<string> beltSortInputs)
    {
      beltSortCalls = beltSortInputs;
    }

    public int Compare(Item left, Item right)
    {
      return 0;
    }
  }
}