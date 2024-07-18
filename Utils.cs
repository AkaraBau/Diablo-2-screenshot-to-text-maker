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

namespace AkarasDegenStuff
{
    public class Utils
    {
        public static string[] DetectFiles(string input)
        {
            string[] allPaths = Directory.GetFiles(input,".PNG" + ".JPG" + ".JPEG");
            return allPaths;
        }
        public static string ShortenString(string input)
        {
            //base type/stats
            string result = input.Replace("DEFENSE: ", "DEF:")
                                 .Replace("REQUIRED LEVEL: ", "LREQ:")
                                 .Replace("VAMPIREFANG BELT", "VB")
                                 .Replace("SHARKSKIN BELT", "SB")
                                 //stats craft roll 
                                 .Replace(" FASTER CAST RATE", "FCR")
                                 .Replace(" TO MANA", "MANA")
                                 .Replace(" TE MANA", "MANA")
                                 .Replace(" T6 MANA", "MANA")
                                 .Replace("REGENERATE MANA ", "MREG:")
                                 //suffixes resistance
                                 .Replace(" POISON LENGTH REDUCED BY", "PLR")
                                 .Replace("LIGHTNING RESIST ", "LR:")
                                 .Replace("FIRE RESIST ", "FR:")
                                 .Replace("POISON RESIST ", "PR:")
                                 .Replace("PEISENN RESIST ", "PR:")
                                 .Replace("POISON ROSIST ", "PR:")
                                 .Replace("COLD RESIST ", "CR:")
                                 .Replace("CORP RESIST ", "CR:")
                                 //suffixes stat/life/mana/rep
                                 .Replace(" TO STRENGTH", "STR")
                                 .Replace(" TE STRENGTH", "STR")
                                 .Replace(" TO LIFO", "LIFE")
                                 .Replace("REPLENISH LIFE ", "REP:")
                                 .Replace("REPLENISH LIFO ", "REP:")
                                 //suffixes other
                                 .Replace(" FASTER HIT RECOVERY", "FHR")
                                 .Replace(" ENHANCED DEFENSE", "ED")
                                 .Replace(" EXTRA GOLD FROM  MONSTERS", "EG")
                                 .Replace(" EXTRA GOLD FROM MONSTERS", "EG")
                                 .Replace("ATTACKER TAKES DAMAGE OF ", "ATDO")
                                 .Replace(" TE LIGHT RADIUS", "LIGHTRADIUS")
                                 .Replace(" CHANCE TO CAST LEVEL ", "CTCLVL")
                                 .Replace(" WHEN STRUCK", "")
                                 .Replace("WHEN STRUCK", "WS")
                                 .Replace("DEFENSE (BASED ON CHARACTER LEVEL) ", "DPL")
                                 .Replace(" MAXIMUM STAMINA", "MS");
            return result;
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
                                 .Replace("PP", "P");
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
    }
}
