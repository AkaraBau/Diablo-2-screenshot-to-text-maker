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
        public int level { get; set; }
        public string stat1 { get; set; }
        public int amount1 { get; set; }
        public string stat2 { get; set; }
        public int amount2 { get; set; }
        public string stat3 { get; set; }
        public int amount3 { get; set; }
        public string stat4 { get; set; }
        public int amount4 { get; set; }
        public string stat5 { get; set; }
        public int amount5 { get; set; }
        public string stat6 { get; set; }
        public int amount6 { get; set; }
        public string stat7 { get; set; }
        public int? amount7 { get; set; }

        public Belt(string[] data)
        {
            string newName = data[1] + data[7].Substring(0, 1) + Utils.GetFirstLetter(data[8]) + Utils.GetFirstLetter(data[9]) + Utils.GetFirstLetter(data[10]) + Utils.GetFirstLetter(data[11]) + Utils.GetFirstLetter(data[12]);
            name = newName;
            type = data[1];
            defense = data[2];
            req1 = data[6];

            amount1 = Utils.ExtractIntFromString(data[7]);
            stat1 = Utils.RemoveNumbers(data[7]);

            amount2 = Utils.ExtractIntFromString(data[8]);
            stat2 = Utils.RemoveNumbers(data[8]);
            
            amount3 = Utils.ExtractIntFromString(data[9]);
            stat3 = Utils.RemoveNumbers(data[9]);

            amount4 = Utils.ExtractIntFromString(data[10]);
            stat4 = Utils.RemoveNumbers(data[10]);
            
            amount5 = Utils.ExtractIntFromString(data[11]);
            stat5 = Utils.RemoveNumbers(data[11]);
            
            amount6 = Utils.ExtractIntFromString(data[12]);
            stat6 = Utils.RemoveNumbers(data[12]);

            if (data.Length == 13)
            {
                amount7 = null;
                stat7 = " ";
            }
            else if (data.Length == 14)
            {
                amount7 = Utils.ExtractIntFromString(data[13]);
                stat7 = Utils.RemoveNumbers(data[13]);
            }
            else if (data.Length < 13 || data.Length > 14)
            {
                throw new ArgumentException("The data array does not contain enough elements.");
            }
        }
        public override string ToString()
        {
            string result = $"{name}/{type}/{defense}/{req1}\t{amount1}{stat1}/{amount2}{stat2}/{amount3}{stat3}/{amount4}{stat4}/{amount5}{stat5}/{amount6}{stat6}";
            if (stat7 != " ")
            {
                result += $"/{amount7}{stat7}";
            }
            return result;
        }
    }
}