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
    public class Utils
    {
        public static string[] DetectFiles(string input)
        {
            string[] allPaths = Directory.GetFiles(input);
            return allPaths;
        }
        public static string ShortenString(string input)
        {
                                 //DPL
            string result = input.Replace("DEFENSEBASEDONCHARACTERLEVEL", "DPL")
                                 .Replace("DEFENSEBASEDONCHARACTERLEVEL", "DPL")
                                 //ED
                                 .Replace("ENHANCEDDEFENSE", "ED")
                                 //defense
                                 .Replace("DEFENSE:", "DEF:")
                                 .Replace("DEFENSE", "DEF:")
                                 //req
                                 .Replace("REQUIREDLEVEL:", "LREQ:")
                                 .Replace("REQUIREDLEVET:", "LREQ:")
                                 .Replace("REQUIREDLEVEL", "LREQ:")
                                 //base
                                 .Replace("VAMPIREFANGBELT", "VB")
                                 .Replace("SHARKSKINBELT", "SB")
                                 //stats craft roll 
                                 .Replace("FASTERCASTRATE", "FCR")
                                 //mana
                                 .Replace("TOMANA", "MANA")
                                 .Replace("TOIMANA", "MANA")
                                 .Replace("TEMANA", "MANA")
                                 .Replace("T6MANA", "MANA")
                                 .Replace("T6IMANA", "MANA")
                                 .Replace("TEIMANA", "MANA")
                                 .Replace("TEINANA", "MANA")
                                 //reg
                                 .Replace("REGENERATEMANA", "MREG:")
                                 .Replace("REGENERATEIMANA", "MREG:")
                                 .Replace("REGENERATEIMIANA", "MREG:")
                                 .Replace("REGENERAMANA", "MREG:")
                                 //PLR
                                 .Replace("POISONLENGTHREDUCEDBY", "PLR")
                                 .Replace("PEISENLENGTHREDUCEDBY", "PLR")
                                 .Replace("POISENLENGTHREDUCEDBY", "PLR")
                                 //LR
                                 .Replace("LIGHTNINGRESIST", "LR:")
                                 //FR
                                 .Replace("FIRERESIST", "FR:")
                                 //PR
                                 .Replace("POISONRESIST", "PR:")
                                 .Replace("POISENRESIST", "PR:")
                                 .Replace("PEISENRESIST", "PR:")
                                 .Replace("POISONROSIST", "PR:")
                                 .Replace("PERSONRESIST", "PR:")
                                 .Replace("PÆISENRESIST", "PR:")
                                 //cold resist
                                 .Replace("COLDRESIST", "CR:")
                                 .Replace("CORPRESIST", "CR:")
                                 .Replace("CERLPRESIST", "CR:")
                                 .Replace("CELPRESIST", "CR:")
                                 .Replace("CERPRESIST", "CR:")
                                 .Replace("COLDR&SIST", "CR:")
                                 //str
                                 .Replace("TOSTRENGTH", "STR")
                                 .Replace("TESTRENGTH", "STR")
                                 //life
                                 .Replace("TOLIFO", "LIFE")
                                 .Replace("TOLIFE", "LIFE")
                                 .Replace("JLIFE", "LIFE")
                                 .Replace("T0OLIFE", "LIFE")
                                 .Replace("TELIFO", "LIFE")
                                 .Replace("TELIFE", "LIFE")
                                 //rep 
                                 .Replace("REPLENISHLIFE", "REP:")
                                 .Replace("REPLENISHLIFO ", "REP:")
                                 //fhr
                                 .Replace("FASTERHITRECOVERY", "FHR")
                                 .Replace("FASTERHITRECOVER", "FHR")
                                 //EG
                                 .Replace("EXTRAGOLDFROMMONSTERS", "EG")
                                 .Replace("EXTRAGOLDFROMMONSTERS", "EG")
                                 //ATDO
                                 .Replace("ATTACKERTAKESDAMAGEOF", "ATDO")
                                 //Light radius
                                 .Replace("TELIGHTRADIUS", "LIGHTRADIUS")
                                 //Ctc ws
                                 .Replace("CHANCETOCASTLEVEL", "CTCLVL")
                                 .Replace("WHENSTRUCK", "")
                                 //stamina 
                                 .Replace("MAXIMUMSTAMINA", "MS")
                                 .Replace("LEAS", "7%")
                                 .Replace("REPAIRSDURABILITYIN33SECONDS", "REPAIR")
                                 .Replace("N0", "26%");
            return result;
        }
        public static string RemoveAllWhiteSpace(string input)
        {
             return Regex.Replace(input, @"[ \t\r\f\v]", "");
        }
        public static string ChangeLetters(string input)
        {
            string result = input.ToUpper()
                                 .Replace("1@", "10")
                                 .Replace('®', 'O')
                                 .Replace('@', 'O')
                                 .Replace("[", "")
                                 .Replace('©', 'O')
                                 .Replace('£', 'E')
                                 .Replace("#", "")
                                 .Replace("+", "")
                                 .Replace("@", "")
                                 .Replace("|", "")
                                 .Replace("*", "")
                                 .Replace("RR", "R")
                                 .Replace("PP", "P")
                                 .Replace("SS", "S")
                                 .Replace("NN", "N")
                                 .Replace("II", "I")
                                 .Replace("EE", "E")
                                 .Replace("É", "E")
                                 .Replace("(", "")
                                 .Replace(")", "")
                                 .Replace(".", "")
                                 .Replace(";", "")
                                 .Replace("}", "")
                                 .Replace("/", "")
                                 .Replace(",", "")
                                 .Replace("]", "")
                                 .Replace("'", "")
                                 .Replace(":", "");
            return result;
        }
        public static void PrintList(List<string> inputlist)
        {
            foreach (var l in inputlist)
            {
                Console.WriteLine(l);
            }
        }
        public static List<string> ObjectToString(List<Belt> inputlist)
        {
            List<string> list = new List<string>();
            foreach (var i in inputlist)
            {
                list.Add(i.ToString());
            }
            return list;
        }
        public static char GetFirstLetter(string input)
        {
            return input.FirstOrDefault(char.IsLetter);
        }
    }
}
