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
        public static string ShortenString (string input){
                                 //base type/stats
            string result = input.Replace("DEFENSE: ", "DEF:")
                                 .Replace("REQUIRED LEVEL: ", "LREQ:")
                                 .Replace("VAMPIREFANG BELT", "VB")
                                 .Replace("SHARKSKIN BELT", "SB")
                                //stats craft roll 
                                 .Replace(" FASTER CAST RATE", "FCR")
                                 .Replace(" TO MANA", "MANA")
                                 .Replace("REGENERATE MANA ", "MREG:")
                                //suffixes resistance
                                 .Replace(" POISON LENGTH REDUCED BY", "PLR")
                                 .Replace("LIGHTNING RESIST ", "LR:")
                                 .Replace("FIRE RESIST ", "FR:")
                                 .Replace("POISON RESIST ", "PR:")
                                 .Replace("COLD RESIST ", "CR:")
                                 //suffixes stat/life/mana/rep
                                 .Replace(" TO STRENGTH", "STR")
                                 .Replace(" TO LIFO", "LIFE")
                                 .Replace("REPLENISH LIFE ", "REP")
                                 //suffixes other
                                 .Replace(" FASTER HIT RECOVERY","FHR")
                                 .Replace(" ENHANCED DEFENSE","ED")
                                 .Replace(" EXTRA GOLD FROM  MONSTERS", "EG")
                                 .Replace(" EXTRA GOLD FROM MONSTERS", "EG")
                                 .Replace("ATTACKER TAKES DAMAGE OF ", "ATDO")
                                 .Replace("DEFENSE (BASED ON CHARACTER LEVEL) ", "DPL");
            return result;
        }
        public static string ChangeLetters(string input)
        {
            string result = input.Replace('e', 'O')
                                 .Replace('®', 'O')
                                 .Replace('@', 'O')
                                 .Replace('o', 'O')
                                 .Replace('[', ' ')
                                 .Replace('©', 'O')
                                 .Replace('£','E')
                                 .Replace('i','I')
                                 .Replace('r','R')
                                 .Replace('p','D')
                                 .Replace('m', 'M');
            return result;
        }
        public static void PrintList(List<Belt> inputlist)
        {
            foreach (var l in inputlist)
            {
                Console.WriteLine(l);
            }
        }
    }
}
