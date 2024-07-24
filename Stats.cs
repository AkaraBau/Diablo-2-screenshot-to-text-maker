using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class Stats
    {
        public string stat { get; set; }
        public int? amount { get; set; }

        public Stats(string data) 
        {
            amount = Utils.ExtractIntFromString(data);
            stat = Utils.RemoveNumbers(data);
            
        }
        public override string ToString () 
        {
                string result = $"{amount}{stat}";
                return result; 
        }
    }
}