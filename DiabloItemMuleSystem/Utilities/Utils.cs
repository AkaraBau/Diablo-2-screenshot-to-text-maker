using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using DiabloItemMuleSystem.Models;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Cryptography.X509Certificates;


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

                    directoryExists = true; // Exit the loop if the directory exists and contains valid files
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Can't find the directory.\n Try again.");
                    input = Console.ReadLine(); 
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
        public static List<Item> SearchForStatAndAmount(List<Item> items, StatType searchStat, int bot, int top)
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
        public static List<string> RemoveListContentBeforeObjectCreationOcr(List<string> inputList) //here to match parsing to ocr so i can use same constructor for both. 
        {
            if (inputList[1] == "SB" || inputList[1] == "VB" || inputList[1] == "SPS" || inputList[1] == "MC" || inputList[1] == "DHS")
            {
                inputList.RemoveAt(0); // 0, 3 , 4 , 5 (index removed)
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
        public static List<Item> TxtFileToListItem(string txtFileData)
        {
            List<Item> itemList = new List<Item>();
            string[] txtFileSplitOnNewline = txtFileData.Split("\n");
            var listData = new List<string>();
            

            for (int i = 0; i < txtFileSplitOnNewline.Length - 1; i++)
            {
                string[] txtFileSplitBeforeItemCreation = txtFileSplitOnNewline[i].Trim().Split('/', '\t');
                listData = new List<string>(txtFileSplitBeforeItemCreation);
                listData.RemoveAt(0);
                
                
                Item item = new Item(listData);
                itemList.Add(item);
            }
           

            return itemList;
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
                try
                {
                    string txtFile = File.ReadAllText(args[1]);
                    allItems = Utils.TxtFileToListItem(txtFile);
                }
                catch (FileNotFoundException) 
                {
                    Console.WriteLine("Couldnt find txt. Program initiated in normal mode."); 
                }
            }

            return allItems; 
        }
        public static void ExportItemsToTxt(List<Item> allItems)
        {
            Console.WriteLine("What would you like to name the file?");
            string name = Console.ReadLine();
            var filePath = UserUtils.GetFilePath("");
            bool txtFileFound = false;
            while (!txtFileFound)
            {
                try
                {
                    filePath = Path.Combine(filePath, name + ".txt");
                    var sItems = Utils.ItemToString(allItems);
                    File.WriteAllLines(filePath, sItems);

                    Console.WriteLine("Txt file created");
                    txtFileFound = true;
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Can't find the .txt");
                    filePath = UserUtils.GetFilePath(".txt");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex + "error occured");
                    break; 
                }
                 
                
            }                    
        }
        public static List<Item>  ParseTxtFileToItem (List<Item>allItems) 
        {
            string filePath = UserUtils.GetFilePath(".txt"); 
            while (true)
            { 
                try
                {
                    string txtFile = File.ReadAllText(filePath); 
                    var mergelist = Utils.TxtFileToListItem(txtFile);
                    allItems.AddRange(mergelist);
                    return allItems;
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("wrong input");
                    filePath = UserUtils.GetFilePath(".txt");
                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("wrong input");
                    filePath = UserUtils.GetFilePath(".txt");
                }
            }

        }
    }
}
