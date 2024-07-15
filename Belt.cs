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

            if (data.Length == 13)
            {
                stat7 = " ";
            }
            else if (data.Length == 14)
            {
                stat7 = data[13];
            }
            else if (data.Length < 13 || data.Length > 14)
            {
                throw new ArgumentException("The data array does not contain enough elements.");
            }
        }
        public override string ToString()
        {
            if (stat7 == " ")
            {
                return $"{name}/{type}/{defense}/{req1}/{stat1}/{stat2}/{stat3}/{stat4}/{stat5}/{stat6}";
            }
            else 
            { 
            return $"{name}/{type}/{defense}/{req1}/{stat1}/{stat2}/{stat3}/{stat4}/{stat5}/{stat6}/{stat7}"; 
            }
        }
    }
}