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
                    // Using Directory.EnumerateFiles to filter by file extensions
                    var validExtensions = new[] { ".png", ".jpeg", ".jpg" };
                    allPaths = Directory.EnumerateFiles(input)
                                        .Where(file => validExtensions.Contains(Path.GetExtension(file).ToLower()))
                                        .ToArray();

                    if (allPaths.Length > 0)
                    {
                        Console.WriteLine("Loading input. Please wait.");
                    }
                    else
                    {
                        Console.WriteLine("No valid files found. Please enter a directory with .PNG, .JPEG, or .JPG files.");
                        input = Console.ReadLine(); // Prompt user for new input
                        continue;
                    }

                    directoryExists = true; // Exit the loop if the directory exists and contains valid files
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Can't find the directory, try again.");
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
                                 .Replace("SPIDERWEBSASH", "SPS")
                                 .Replace("MITHRILCOIL", "MC")
                                 .Replace("DEMONHIDESASH", "DHS")
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
                                 .Replace("PEISONRESIST", "PR")
                                 .Replace("PERSONRESIST", "PR")
                                 .Replace("PÆISENRESIST", "PR")
                                 .Replace("PERSENRESIST", "PR")
                                 .Replace("PE1ISONRESIST", "PR")
                                 //cold resist
                                 .Replace("COLDRESIST", "CR")
                                 .Replace("CORPRESIST", "CR")
                                 .Replace("CERLPRESIST", "CR")
                                 .Replace("CELDRESIST", "CR")
                                 .Replace("CELNRESIST", "CR")
                                 .Replace("CELPRESIST", "CR")
                                 .Replace("1CERPRESIST", "CR")
                                 .Replace("CERPRESIST", "CR")
                                 .Replace("CERPDRESIST", "CR")
                                 .Replace("COLDR&SIST", "CR")
                                 .Replace("COLORESIST", "CR")
                                 .Replace("COLPRESIST", "CR")
                                 //all res 
                                 .Replace("ALLRESISTANCES", "@res")
                                 .Replace("ALLRESISTANCES", "@res")
                                 .Replace("AELRESISTANCES", "@res")
                                 // dex 
                                 .Replace("TEDEXTERITY", "DEX")
                                 .Replace("TODEXTERITY", "DEX")
                                 //str
                                 .Replace("TOSTRENGTH", "STR")
                                 .Replace("TESTRENGTH", "STR")
                                 .Replace("TOOSTRENGTH", "STR")
                                 .Replace("7OSTRENGTH", "STR")
                                 //life
                                 .Replace("TOLIFO", "LIFE")
                                 .Replace("TOLIFE", "LIFE")
                                 .Replace("JLIFE", "LIFE")
                                 .Replace("T0OLIFE", "LIFE")
                                 .Replace("TELIFO", "LIFE")
                                 .Replace("TELIFE", "LIFE")
                                 .Replace("TAELIFE", "LIFE")
                                 .Replace("TELIRE", "LIFE")
                                 .Replace("10YLIFE", "LIFE")
                                 .Replace("TOLRFE", "LIFE")
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
                                 .Replace("TOLIGHTRADIUS", "LIGHTRADIUS")
                                 //Ctc ws
                                 .Replace("CHANCETOCASTLEVEL", "CTCLVL")
                                 .Replace("WHENSTRUCK", "")
                                 //stamina 
                                 .Replace("MAXIMUMSTAMINA", "MS")
                                 .Replace("ADDS", "")
                                 .Replace("COLDDAMAGE", "CDMG")
                                 .Replace("DAMAGEREDUCEDBY", "DR")
                                 .Replace("DAMACEREDUCEDBY", "DR")
                                 .Replace("LEAS", "7%")
                                 .Replace("REPAIRSDURABILITYIN33SECONDS", "BENUSTEATTACKRATING")
                                 .Replace("N0", "26%")
                                 //attack raiting 
                                 .Replace("TOATTACKRATING", "AR")
                                 .Replace("TEATTACKRATING", "AR")
                                 .Replace("BONUSAR", "%AR")
                                 .Replace("BENUSAR", "%AR")
                                 //damage
                                 .Replace("TEMINIMUMDAMAGE", "MIN")
                                 .Replace("TEMAXIMUMDAMAGE", "MAX")
                                 //mf
                                 .Replace("BETTERCHANCEOFGETTINGMAGICITEMS", "MF")
                                 .Replace("BETTERCHANCEOFGETTINGMACICITEMS", "MF")
                                 .Replace("BETTERCHANCEOFGETTINGMAAGICITEMS", "MF")
                                 .Replace("BETTERCHANCEOFGETTINGMACGICITEMS", "MF")
                                 //ll ml 
                                 .Replace("LIFESTOLENPERHIT", "LL")
                                 .Replace("MANAAFTEREACHKILL", "MAEK")
                                 .Replace("MANASTOLENPERHIT", "ML")
                                 .Replace("INANASTOLENPERHIT", "ML")
                                 //charges 
                                 .Replace("LEVELCHARGEDBOLTCHARGES", "CHARGES")
                                 //other
                                 .Replace("MACICDR", "MDR")
                                 .Replace("MAACICDAMACEREPUCEBY","MDR");

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
                                 .Replace("1O", "10")
                                 .Replace("10O", "100")
                                 .Replace("1O0", "100")
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
                                 .Replace("“", "")
                                 .Replace("<", "")
                                 .Replace(">", "")
                                 .Replace("-", "")
                                 .Replace("—", "")
                                 .Replace("=", "");
            return result;
        }
        public static void PrintList(List<string> inputlist)
        {
            foreach (var l in inputlist)
            {
                Console.WriteLine(l);
            }
        }
        public static List<string> ItemToString(List<Item> inputlist)
        {
            List<string> list = new List<string>();
            foreach (var i in inputlist)
            {
                list.Add(i.ToString());
            }
            return list;
        }
        public static List<string> StatsToString(List<Stats> inputlist)
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
        public static List<Item> MultiBeltOcr(string input)
        {
            List<Item> itemList = new List<Item>();
            var stringList = new List<string>(); //list of <String>
            string[] splitData = new string[14];
            string[] massInput = Utils.DetectFiles(input);
            string[] massOutput = new string[massInput.Length]; //array of strings containing output from tessaract 

            for (int i = 0; i < massInput.Length; i++)
            {
                using (var stream = Tesseract.ImageToTxt(massInput[i], languages: new[] { Language.English, Language.French }))
                {

                    //loading bar
                    Utils.ProgressBar(i + 1, massInput.Length, 10);
                    //ocr function
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string
                    massOutput[i] = reader.ReadToEnd(); //making stream -> string []

                    massOutput[i] = Utils.RemoveAllWhiteSpace(massOutput[i]); // remove all whitespace
                    massOutput[i] = Utils.ChangeLetters(massOutput[i]); //shorten method
                    massOutput[i] = Utils.ShortenString(massOutput[i]); //shorten method

                    splitData = massOutput[i].Split(new[] { '\n' }, StringSplitOptions.None); //splitting string into string []
                    splitData = splitData.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace

                    Item belt = new Item(splitData); //Creation of belt
                    itemList.Add(belt); //adding belt to list 
                }
            }
            Console.WriteLine("\nDone.");
            return itemList;
        }
        public static void ProgressBar(int progress, int total, int width)
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

                    Console.Write(new string('=', progressWidth - 2)); // Console.Write(new string('#', progressWidth));
                    if (progressWidth == width)
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
