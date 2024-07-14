using System;
using System.Collections.Generic;
using System.IO;
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
        public string size { get; set; }
        public string durability { get; set; }
        public string req1 { get; set; }
        public string req2 { get; set; }
        public string stat1 { get; set; }
        public string stat2 { get; set; }
        public string stat3 { get; set; }
        public string stat4 { get; set; }
        public string stat5 { get; set; }
        public string stat6 { get; set; }
        public string stat7 { get; set; }

        public Belt(string[] data)
        {
            if (data.Length >= 14)
            {
                name = data[0];
                type = data[1];
                defense = data[2];
                size = data[3];
                durability = data[4];
                req1 = data[5];
                req2 = data[6];
                stat1 = data[7];
                stat2 = data[8];
                stat3 = data[9];
                stat4 = data[10];
                stat5 = data[11];
                stat6 = data[12];
                stat7 = data[13];
            }
            else if (data.Length == 13)
            {
                name = data[0];
                type = data[1];
                defense = data[2];
                size = data[3];
                durability = data[4];
                req1 = data[5];
                req2 = data[6];
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
    }
}