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
using Microsoft.VisualBasic;

namespace DiabloItemMuleSystem
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
                                 .Replace("QDEFBASEDSNCHARACTERLEVEL", "DPL")
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
                                 .Replace("10LIFE", "LIFE")
                                 //rep 
                                 .Replace("REPLENISHLIFE", "REP")
                                 .Replace("REPLENISHLIFO ", "REP")
                                 //fhr
                                 .Replace("FASTERHITRECOVERY", "FHR")
                                 .Replace("FASTERHITRECOVER", "FHR")
                                 .Replace("FASTERHIFRECOVERY", "FHR")
                                 .Replace("FASTERHITRECEVERY", "FHR")
                                 //EG
                                 .Replace("EXTRAGOLDFROMMONSTERS", "GOLD")
                                 .Replace("EXTRAGOLDFROMMONSTERS", "GOLD")
                                 //ATDO
                                 .Replace("ATTACKERTAKESDAMAGEOF", "ATDO")
                                 //Light radius
                                 .Replace("TELIGHTRADIUS", "LIGHTRADIUS")
                                 .Replace("TOLIGHTRADIUS", "LIGHTRADIUS")
                                 //Ctc ws
                                 .Replace("CHANCETOCASTLEVEL", "CTC")
                                 .Replace("WHENSTRUCK", "")
                                 .Replace("NOVA", "")
                                 //stamina 
                                 .Replace("MAXIMUMSTAMINA", "MS")
                                 .Replace("ADDS", "")
                                 .Replace("COLDDAMAGE", "CDMG")
                                 .Replace("DAMAGEREDUCEDBY", "DR")
                                 .Replace("DAMACEREDUCEDBY", "DR")
                                 .Replace("LEAS", "7%")
                                 .Replace("REPAIRSDURABILITYIN33SECONDS", "")
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
                                 .Replace("MAACICDAMACEREPUCEBY", "MDR")
                                 .Replace("QDPL", "DPL");

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
                                 .Replace("1O", "10")
                                 .Replace("10O", "100")
                                 .Replace("1O0", "100")
                                 .Replace('®', 'O')
                                 .Replace('@', 'O')
                                 .Replace('©', 'O')
                                 .Replace('£', 'E')
                                 .Replace("RR", "R")
                                 .Replace("PP", "P")
                                 .Replace("SS", "S")
                                 .Replace("NN", "N")
                                 .Replace("II", "I")
                                 .Replace("EE", "E")
                                 .Replace("É", "E")
                                 .Replace("[", "")
                                 .Replace("#", "")
                                 .Replace("+", "")
                                 .Replace("@", "")
                                 .Replace("|", "")
                                 .Replace("*", "")
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
                                 .Replace("=", "")
                                 .Replace("°", "");
            return result;
        }
        public static void PrintList(List<Item> inputlist)
        {
            foreach (var l in inputlist)
            {
                Console.WriteLine(l.ToString());
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
        public static List<string> SingleBeltOcr(string input)
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
                List<string> listData = new List<string>(data);
                Console.WriteLine("item added");
                return listData;
            }
        }
        public static List<Item> MultiItemOcr(string input)
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
                    List <string> listSplitData = new List<string> (splitData);
                    listSplitData = Utils.RemoveListContentBeforeObjectCreationOcr(listSplitData);

                    Item belt = new Item(listSplitData); //Creation of belt
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
            Console.Write($"{progress}/{total}");

        }
        public static int CompareStat(Stats inputA, Stats inputB)
        {

            if (inputA == null && inputB == null)
            {
                return 0;
            }
            else if (inputA == null)
            {
                return 1;
            }
            else if (inputB == null)
            {
                return -1;
            }
            else if (inputA != null && inputB != null)
            {
                var result = (int)inputB.Amount - (int)inputA.Amount;

                return result;
            }

            return 0;
        }
        public static int CompareMultipleStats(Item left, Item right, string[] sortParameters)
        {


            for (int i = 0; i < sortParameters.Length; i++)
            {
                Stats statLeft = left.GetStat(sortParameters[i]);
                Stats statRight = right.GetStat(sortParameters[i]);

                int result = Utils.CompareStat(statLeft, statRight);

                if (result != 0)
                {
                    return result;
                }

            }
            return 0;
        }
        public static bool CheckEqualStats(List<Stats> left, List<Stats> right)
        {



            if (left.Count != right.Count) return false;

            for (int i = 0; i < left.Count - 1; i++)
            {
                if (!left[i].Equals(right[i]))
                {
                    return false;
                }
            }



            return true;

        }
        public static List<Item> SearchForStatAndAmount (List<Item> items, string searchStat, int bot, int top) 
        {
            List<Item> result = new List<Item>();
            Stats stat = null; 
            for (int i = 0; i < items.Count-1; i++)
            {
                stat = items[i].GetStat(searchStat);
                if (stat != null) 
                {
                    if (items[i].GetAmount(bot, top, stat) != null) 
                    { 
                    result.Add(items[i]);
                    }
                }
                
            }
            return result; 
        }
        public static List<string> RemoveListContentBeforeObjectCreationOcr (List<string> inputList)
        {
            if (inputList[1] == "SB" || inputList[1] == "VB" || inputList[1] == "SPS" || inputList[1] == "MC" || inputList[1] == "DHS")
            {
                inputList.RemoveAt(0); // 0, 3 , 4 , 5 
                inputList.RemoveAt(1);
                inputList.RemoveAt(1);
                inputList.RemoveAt(1);
                inputList.RemoveAt(1);
                return inputList;
            }
            else if (inputList[1] == "AMULET" || inputList[1] == "RING")
            {
                inputList.RemoveAt(0);
            }
            else if (inputList[1] == "JEWEL" )
            {
                inputList.RemoveAt(0);
                inputList.RemoveAt(1);
            }
            return inputList; 
        }
        public static List<Item> TxtFileToListItem (string txtFile)
        {
            List<Item> list = new List<Item>();
            string[] txtFileSplitOnNewline = txtFile.Split("\n");

            for (int i = 0; i < txtFileSplitOnNewline.Length-1; i++)
            {
                string[] txtFileSplitBeforeItemCreation = txtFileSplitOnNewline[i].Split('/', '\t');
                List<string> listData = new List<string>(txtFileSplitBeforeItemCreation);
                listData.RemoveAt(0);
                Item item = new Item(listData);
                list.Add(item);
            }

            return list;
        }
        public static void AddItemToDatabase(Item item)
        {
            var ItemContext = new ItemDbContext();

            ItemContext.ItemTable.Add(item);

            foreach (var stats in item.ListOfStats)
            {
                ItemContext.StatsTable.Add(stats);
            }
            
            ItemContext.SaveChanges(); 
        }
    }
}
