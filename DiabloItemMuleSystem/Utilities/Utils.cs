using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using DiabloItemMuleSystem.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace DiabloItemMuleSystem.Utilities
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
        public static void StatusOcr(int progress, int total) 
        {
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

                int result = CompareStat(statLeft, statRight);

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
        public static List<Item> SearchForStatAndAmount(List<Item> items, string searchStat, int bot, int top)
        {
            List<Item> result = new List<Item>();
            Stats stat = null;
            for (int i = 0; i < items.Count - 1; i++)
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
        public static List<string> RemoveListContentBeforeObjectCreationOcr(List<string> inputList)
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
            else if (inputList[1] == "JEWEL")
            {
                inputList.RemoveAt(0);
                inputList.RemoveAt(1);
            }
            return inputList;
        }
        public static List<Item> TxtFileToListItem(string txtFile)
        {
            List<Item> list = new List<Item>();
            string[] txtFileSplitOnNewline = txtFile.Split("\n");

            for (int i = 0; i < txtFileSplitOnNewline.Length - 1; i++)
            {
                string[] txtFileSplitBeforeItemCreation = txtFileSplitOnNewline[i].Split('/', '\t');
                List<string> listData = new List<string>(txtFileSplitBeforeItemCreation);
                listData.RemoveAt(0);
                Item item = new Item(listData);
                list.Add(item);
            }

            return list;
        }
        public static List<Item> Initiation(string[] args)
        {
            List<Item> allItems = new List<Item>();

            if (args[0] == "ocr")
            {
                allItems = Ocr.MultiScan(args[1]);               
            }
            else if (args[0] == "parse")
            {
                string txtFile = File.ReadAllText(args[1]);
                allItems = Utils.TxtFileToListItem(txtFile);    
            }

            return allItems; 
        }
    }
}
