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
            string[] allPaths = null;
            bool directoryExists = false;

            while (!directoryExists)
            {
                try
                {
                    allPaths = Directory.GetFiles(input);
                    Console.WriteLine("Loading input. Please wait.");
                    directoryExists = true; // Exit the loop if the directory exists
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Cant find the directory try again.");
                    input = Console.ReadLine(); // Prompt user for new input
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    break; // Exit the loop if an unexpected error occurs
                }
            }
            return allPaths;
        }
        public static string ShortenString(string input)
        {
            //DPL
            string result = input.Replace("DEFENSEBASEDONCHARACTERLEVEL", "DPL")
                                 .Replace("DEFENSEBASEDONCHARACTERLEVEL", "DPL")
                                 .Replace("DERENSEBASEDONCHARACTERLEVEL", "DPL")
                                 //ED
                                 .Replace("ENHANCEDDEFENSE", "ED")
                                 //defense
                                 .Replace("DEFENSE:", "DEF")
                                 .Replace("DEFENSE", "DEF")
                                 .Replace("DERENSE", "DEF")
                                 .Replace("DERENSECT", "DEF")
                                 //req
                                 .Replace("REQUIREDLEVEL:", "LREQ")
                                 .Replace("REQUIREDLEVET:", "LREQ")
                                 .Replace("REQUIREDLEVEL", "LREQ")
                                 .Replace("REQUIREDLEVET", "LREQ")
                                 //base
                                 .Replace("VAMPIREFANGBELT", "VB")
                                 .Replace("VANPIREFANGBELT", "VB")
                                 .Replace("MANPIREFANGBELT", "VB")
                                 .Replace("SHARKSKINBELT", "SB")
                                 .Replace("SHARESKINBELT", "SB")
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
                                 .Replace("IREMANA", "MANA")
                                 //reg
                                 .Replace("REGENERATEMANA", "MREG")
                                 .Replace("REGENERATEIMANA", "MREG")
                                 .Replace("REGENERATEIMIANA", "MREG")
                                 .Replace("REGENERAMANA", "MREG")
                                 .Replace("REGEMERAMANA", "MREG")
                                 .Replace("REGENERATEMAMA", "MREG")
                                 //PLR
                                 .Replace("POISONLENGTHREDUCEDBY", "PLR")
                                 .Replace("POISONLENGTH", "PLR")
                                 .Replace("PEISENLENGTHREDUCEDBY", "PLR")
                                 .Replace("POISENLENGTHREDUCEDBY", "PLR")
                                 .Replace("PEISENLENGTHREDUCEDSV", "PLR")
                                 //LR
                                 .Replace("LIGHTNINGRESIST", "LR")
                                 .Replace("LIGHTHINGRESIST", "LR")
                                 //FR
                                 .Replace("FIRERESIST", "FR")
                                 //PR
                                 .Replace("POISONRESIST", "PR")
                                 .Replace("POISENRESIST", "PR")
                                 .Replace("PEISENRESIST", "PR")
                                 .Replace("POISONROSIST", "PR")
                                 .Replace("PERSONRESIST", "PR")
                                 .Replace("PÆISENRESIST", "PR")
                                 //cold resist
                                 .Replace("COLDRESIST", "CR")
                                 .Replace("CORPRESIST", "CR")
                                 .Replace("CERLPRESIST", "CR")
                                 .Replace("CELDRESIST", "CR")
                                 .Replace("CELNRESIST", "CR")
                                 .Replace("CELPRESIST", "CR")
                                 .Replace("CERPRESIST", "CR")
                                 .Replace("CERPDRESIST", "CR")
                                 .Replace("COLDR&SIST", "CR")
                                 .Replace("COLORESIST", "CR")
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
                                 .Replace("TAELIFE", "LIFE")
                                 //rep 
                                 .Replace("REPLENISHLIFE", "REP")
                                 .Replace("REPLENISHLIFO ", "REP")
                                 //fhr
                                 .Replace("FASTERHITRECOVERY", "FHR")
                                 .Replace("FASTERHITRECOVER", "FHR")
                                 .Replace("FASTERHIFRECOVERY", "FHR")
                                 .Replace("FASTERHITRECEVERY", "FHR")
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
                                 .Replace(":", "")
                                 .Replace("%", "")
                                 .Replace("«", "")
                                 .Replace("-", "")
                                 .Replace("“", "");
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
        public static int ExtractIntFromString(string input)
        {
            string pattern = @"\d+";
            string combinedNumbers = "";
            MatchCollection matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                combinedNumbers += match.Value;
            }
            int.TryParse(combinedNumbers, out int result);

            return result;

        }
        public static string RemoveNumbers(string input)
        {
            string pattern = @"\d";
            string result = Regex.Replace(input, pattern, "");
            return result;
        }
        public static string[] SingleBeltOcr(string input)
        {
            string item = null;
            string[] data = new string[14];
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string

                item = reader.ReadToEnd(); //making stream -> string 
                item = Utils.RemoveAllWhiteSpace(item);
                item = Utils.ChangeLetters(item);
                item = Utils.ShortenString(item);
                data = item.Split(new[] { '\n' }, StringSplitOptions.None); //split string into array of strings 
                data = data.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace
                Console.WriteLine("item added");
                return data;
            }
        }
        public static List<string> MultiBeltOcr(string input)
        {
            List<Belt> beltList = new List<Belt>();
            var stringList = new List<string>(); //list of <String>
            string[] splitData = new string[14];
            string[] massInput = Utils.DetectFiles(input);
            string[] massOutput = new string[massInput.Length]; //array of strings containing output from tessaract 

            for (int i = 0; i < massInput.Length; i++)
            {
                using (var stream = Tesseract.ImageToTxt(massInput[i], languages: new[] { Language.English, Language.French }))
                {

                    //loading bar
                    Utils.PenisProgressBar(i + 1, massInput.Length, 10);
                    //ocr function
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string
                    massOutput[i] = reader.ReadToEnd(); //making stream -> string []

                    massOutput[i] = Utils.RemoveAllWhiteSpace(massOutput[i]); // remove all whitespace
                    massOutput[i] = Utils.ChangeLetters(massOutput[i]); //shorten method
                    massOutput[i] = Utils.ShortenString(massOutput[i]); //shorten method

                    splitData = massOutput[i].Split(new[] { '\n' }, StringSplitOptions.None); //splitting string into string []
                    splitData = splitData.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace

                    Belt belt = new Belt(splitData); //Creation of belt
                    beltList.Add(belt); //adding belt to list 
                }
            }
            Console.WriteLine("\nDone.");
            stringList = Utils.ObjectToString(beltList);
            return stringList;
        }
        public static void PenisProgressBar(int progress, int total, int width)
        {

            int progressWidth = (int)((double)progress / total * width);
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, Console.CursorTop);

            Console.Write("[");
            if (progressWidth > 0)
            {
                Console.Write('C');
                if (progressWidth > 1)
                {
                    Console.Write(new string('=', progressWidth - 2));
                    if (progressWidth == 10)
                    {
                        Console.Write("3");
                    }
                }
            }
            Console.Write(new string(' ', width - progressWidth));
            Console.Write($"]{progress}/{total}");
        }
    }
}
