using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class Belt
    {
        public string name { get; set; }
        public string type { get; set; }
        public string defense { get; set; }
        public string req1 { get; set; }
        public string stat1 { get; set; }
        public string stat2 { get; set; }
        public string stat3 { get; set; }
        public string stat4 { get; set; }
        public string stat5 { get; set; }
        public string stat6 { get; set; }
        public string stat7 { get; set; }

        public Belt(string[] data)
        {
            if (data.Length >= 13)
            {
                name = data[0];
                type = data[1];
                defense = data[2];
                req1 = data[6];
                stat1 = data[7]; 
                stat2 = data[8];
                stat3 = data[9];
                stat4 = data[10];
                stat5 = data[11];
                stat6 = data[12];
                stat7 = data[13];
            }
            else if (data.Length == 12)
            {
                name = data[0];
                type = data[1];
                defense = data[2];
                req1 = data[6];
                stat1 = data[7];
                stat2 = data[8];
                stat3 = data[9];
                stat4 = data[10];
                stat5 = data[11];
                stat6 = data[12];
                stat7 = " ";
            }
            else
            {
                throw new ArgumentException("The data array does not contain enough elements.");
            }
        }
        public override string ToString()
        {
            return $"{name}\n{type}\n{defense}\n{req1}\n{stat1}\n{stat2}\n{stat3}\n{stat4}\n{stat5}\n{stat6}\n{stat7}";
        }
    }
}