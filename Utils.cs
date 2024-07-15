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
            string result = input.Replace("DEFENSE:", "def")
                                 .Replace("VAMPIREFANG BELT", "VB")
                                 .Replace("FASTER CAST RATE", "fcr")
                                 .Replace("TO STRENGTH", "str")
                                 .Replace("TO LIFE", "life")
                                 .Replace("TO MANA", "mana")
                                 .Replace("REGENERATE MANA", "reg")
                                 .Replace("LIGHTNING RESIST", "LR")
                                 .Replace("FIRE RESIST", "FR")
                                 .Replace("POISON RESIST", "PR")
                                 .Replace("COLD RESIST", "CR");

            return result;
        }
        public static string ChangeLetters(string input)
        {
            string result = input.Replace('e', 'O')
                                 .Replace('®', 'O')
                                 .Replace('@', 'O')
                                 .Replace('o', 'O')
                                 .Replace('[', ' ')
                                 .Replace('©', 'O');
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