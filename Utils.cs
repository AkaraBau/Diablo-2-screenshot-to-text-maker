using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class Utils
    {
        public static void PrintList(List<Belt>)
        {
            foreach (var l in List)
            {
            Console.WriteLine(l); 
            }

        }
    }
}