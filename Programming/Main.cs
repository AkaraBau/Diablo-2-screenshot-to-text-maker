using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;

namespace DiabloItemMuleSystem
{
    public class Main
    {
        public static void DoIt(string[]args)
        {
            string input = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new";
            List<string> sItems = new List<string>(); //list of <String>
            List<Item> allItems = new List<Item>(); //list of <Belt>
            string[] sortParameters = new string[] { "FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "GOLD" };
            string filePath = null;
            string command = null;

            if (args.Length == 0) return;
            else if (args.Length <= 2)
            {
                command = args[0];
                filePath = args[1];
            }


            if (command == "ocr")
            {
                allItems = Utils.MultiItemOcr(filePath);
                sItems = Utils.ItemToString(allItems);

            }
            else if (command == "parse")
            {
                string txtFile = File.ReadAllText(filePath);
                allItems = Utils.TxtFileToListItem(txtFile);
                sItems = Utils.ItemToString(allItems);
            }



            Console.WriteLine("What would you like to do?");
            Console.WriteLine("print[p], txt[t], sortby[sb], generic belt sort[gbs], add[a], add multiple[am], add from txt [at], search for stat and specific amount [sss], remove [r], quit[q]");
            String call = null;
            while (call != "q")
            {
                call = Console.ReadLine();
                if (call == "p")
                {
                    Utils.PrintList(allItems);
                }
                else if (call == "t")
                {
                    Console.WriteLine("What would you like to name the file?");
                    String name = Console.ReadLine();

                    Console.WriteLine("What directory should the file get created in?");
                    Console.WriteLine("Format:" + @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have");
                    input = Console.ReadLine();

                    input = Path.Combine(input, name + ".txt");
                    sItems = Utils.ItemToString(allItems);
                    File.WriteAllLines(input, sItems);

                    Console.WriteLine("Txt file created"); 
                }
                else if (call == "sb")
                {
                    Console.WriteLine("What stat would you like to sort by? ");
                    var sortCall = Console.ReadLine().ToUpper().Trim();
                    allItems.Sort(new SortByStat(sortCall));                   
                }
                else if (call == "a")
                {
                    Console.WriteLine("What image would you like to add?\n" + @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new\sln.PNG");
                    input = Console.ReadLine();

                    Item item = new Item(Utils.SingleBeltOcr(input));
                    allItems.Add(item);
                }
                else if (call == "am")
                {
                    Console.WriteLine("What directory would you like to scan? Format below \n" + input);
                    input = Console.ReadLine();

                    var mergeList = Utils.MultiItemOcr(input); //multi scan method
                    allItems.AddRange(mergeList); //adding output to list
                    
                }
                else if (call == "at")
                {
                    Console.WriteLine("Input filepath to txt file.");
                    input = Console.ReadLine();

                    string txtFile = File.ReadAllText(input);
                    var mergelist = Utils.TxtFileToListItem(txtFile);
                    allItems.AddRange(mergelist); 
                }
                else if (call == "gbs")
                {

                    allItems.Sort(new GenericItemSort(sortParameters));
                    Console.WriteLine("Sorted");
                }
                else if (call == "sss")
                {
                    Console.WriteLine("How many different stats would you like to search for?");
                    int howManyStats = Convert.ToInt32(Console.ReadLine());
                    int top = 0;
                    int bottom = 0;
                    string statForSearch = null;
                    List<Item> searchedList = new List<Item>();                   


                    for (int i = 0; i < howManyStats; i++)
                    {
                        Console.WriteLine("Set a bottom range");
                        bottom = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Set a top range");
                        top = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("set which stat to search for");
                        statForSearch = Console.ReadLine();
                        if (i == 0)
                        {
                            searchedList = Utils.SearchForStatAndAmount(allItems, statForSearch, bottom, top);
                        }
                        if (i >= 1)
                        {
                            searchedList = Utils.SearchForStatAndAmount(searchedList, statForSearch, bottom, top);
                        }                                                   
                    }

                    searchedList.Sort(new GenericItemSort(sortParameters)); 
                    Utils.PrintList(searchedList);

                    if (searchedList.Count == 0) 
                    { 
                        Console.WriteLine("No items with those stats"); 
                    }
                    

                }
                else if (call == "r")
                {
                    Console.WriteLine("Which id would you like to remove?");
                    int remove = Convert.ToInt32(Console.ReadLine());
                    allItems.RemoveAll(item => item.Id == remove); 
                    
                }
                else if (call == "q")
                {
                    return;
                }
                else
                {
                    Console.WriteLine("wrong input try again");
                }
            }
            Console.ReadLine();
        }
    }
}