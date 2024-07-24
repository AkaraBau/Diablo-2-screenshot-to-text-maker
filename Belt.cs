using System;
using System.Collections.Generic;
using System.Formats.Asn1;
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
        public int defenseAmount { get; set; }
        public string req1 { get; set; }
        public int level { get; set; }
        public string stat1 { get; set; }
        public int? amount1 { get; set; }
        public string stat2 { get; set; }
        public int? amount2 { get; set; }
        public string stat3 { get; set; }
        public int? amount3 { get; set; }
        public string stat4 { get; set; }
        public int? amount4 { get; set; }
        public string stat5 { get; set; }
        public int? amount5 { get; set; }
        public string stat6 { get; set; }
        public int? amount6 { get; set; }
        public string stat7 { get; set; }
        public int? amount7 { get; set; }

        public Belt(string[] data)
        {
            type = data[1];

            defenseAmount = Utils.ExtractIntFromString(data[2]);
            defense = Utils.RemoveNumbers(data[2]);

            level = Utils.ExtractIntFromString(data[6]);
            req1 = Utils.RemoveNumbers(data[6]);

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

            if (data.Length == 9)
            {
                amount3 = null;
                stat3 = "";
                amount4 = null;
                stat4 = "";
                amount5 = null;
                stat5 = "";
                amount6 = null;
                stat6 = "";
                amount7 = null;
                stat7 = "";
            }
            else if (data.Length == 10)
            {
                amount4 = null;
                stat4 = "";
                amount5 = null;
                stat5 = "";
                amount6 = null;
                stat6 = "";
                amount7 = null;
                stat7 = "";
            }
            else if (data.Length == 11)
            {
                amount5 = null;
                stat5 = "";
                amount6 = null;
                stat6 = "";
                amount7 = null;
                stat7 = "";
            }
            else if (data.Length == 12)
            {
                amount6 = null;
                stat6 = "";
                amount7 = null;
                stat7 = "";
            }
            else if (data.Length == 13)
            {
                amount7 = null;
                stat7 = "";
            }
            else if (data.Length == 14)
            {
                amount7 = Utils.ExtractIntFromString(data[13]);
                stat7 = Utils.RemoveNumbers(data[13]);
            }
            else if (data.Length < 8 || data.Length > 14)
            {
                throw new ArgumentException("The data array does not contain enough elements.");
            }
            ////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////
            string newName = $"{amount1}{stat1}{stat2}{stat3}{stat4}{stat5}{stat6}";
            if (stat2 == "FCR" || stat2 == "OFCR" || data.Length >= 13)
            {
                newName = $"{amount2}{stat2}{stat3}{stat4}{stat5}{stat6}";
            }
            else if (stat2 == "CDMG")
            {
                newName = $"{amount1}{stat1}{stat3}{stat4}{stat5}{stat6}";
            }

            if (data.Length < 13)
            {
                newName = $"{stat1}{stat2}";
            }
            name = newName;
        }
        public override string ToString()
        {
            string result = $"{name}/{type}/{defenseAmount}{defense}/{level}{req1}\t{amount1}{stat1}/{amount2}{stat2}";
            if (stat3 != "")
            {
                result += $"/{amount3}{stat3}";
            }
            else if (stat4 != "")
            {
                result += $"/{amount4}{stat4}";
            }
            else if (stat5 != "")
            {
                result += $"{amount5}{stat5}";
            }
            else if (stat6 != "")
            {
                result += $"/{amount6}{stat6}";
            }
            else if (stat7 != "")
            {
                result += $"/{amount7}{stat7}";
            }
            return result;
        }
    }
}