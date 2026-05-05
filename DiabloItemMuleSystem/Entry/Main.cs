using System;
using System.Collections.Generic;
using System.IO;
using DiabloItemMuleSystem.Utilities;
using DiabloItemMuleSystem.Services;
using DiabloItemMuleSystem.Models;
using System.Security;


namespace DiabloItemMuleSystem.Entry
{
    public class Main
    {
        public static void DoIt(string[] args)
        {

            string[] sortParameters = ["FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "GOLD"];

            List<Item> allItems = Utils.Initiation(args);
            List<string> sItems = Utils.ItemToString(allItems); 


            Console.WriteLine("What would you like to do?");
            Console.WriteLine("print[P], " +
                              " create txt[T], " +
                              "sort by single stat[SB], " +
                              "generic belt sort[GBS]," +
                              " ocr one[OCR], " +
                              "ocr all from folder[OCRALL], " +
                              "add from txt [AT], " +
                              "search for stat and specific amount [SSS], " +
                              "remove by id [R], " +
                              "add all to database [DB]," +
                              "get from database [GET] " +
                              "Delete content of database [DELETE] " +
                              "quit[Q]");
            string call = null;
            while (call != "Q")
            {
                string filePath = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new";
                call = Console.ReadLine();
                if (call == "P")
                {
                    Utils.PrintList(allItems);
                }
                else if (call == "T")
                {
                    Console.WriteLine("What would you like to name the file?");
                    string name = Console.ReadLine();
                    filePath = Utils.GetFilePathFromUser(""); 
                    filePath = Path.Combine(filePath, name + ".txt");
                    sItems = Utils.ItemToString(allItems);
                    File.WriteAllLines(filePath, sItems);

                    Console.WriteLine("Txt file created");
                }
                else if (call == "SB")
                {
                    Console.WriteLine("What stat would you like to sort by? ");
                    var sortCall = Console.ReadLine().ToUpper().Trim();
                    allItems.Sort(new SortByStat(sortCall));
                }
                else if (call == "OCR")
                {
                    filePath = Utils.GetFilePathFromUser(".png"); 
                    Item item = new Item(Ocr.SingleScan(filePath));
                    allItems.Add(item);
                }
                else if (call == "OCRALL")
                {
                     
                    filePath = Utils.GetFilePathFromUser("");
                    var mergeList = Ocr.MultiScan(filePath);
                    allItems.AddRange(mergeList); 

                }
                else if (call == "AT")
                {
                    filePath = Utils.GetFilePathFromUser(".txt"); 
                    string txtFile = File.ReadAllText(filePath);
                    var mergelist = Utils.TxtFileToListItem(txtFile);
                    allItems.AddRange(mergelist);
                }
                else if (call == "GBS")
                {
                    allItems.Sort(new GenericItemSort(sortParameters));
                    Console.WriteLine("Sorted");
                }
                else if (call == "SSS")  //// TODO shit works but is cluttery, looks messy and hard to understand should be able to be optimized. 
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
                else if (call == "R")
                {
                    Console.WriteLine("Which id would you like to remove?");
                    int remove = Convert.ToInt32(Console.ReadLine());
                    allItems.RemoveAll(item => item.Id == remove);

                }
                else if (call == "GET")
                {
                    allItems = Database.GetItems(); 

                }
                else if (call == "DB")
                {
                    foreach (var item in allItems)
                    {
                        Database.AddItem(item);
                    }
                }
                else if (call == "DELETE")
                {
                    Database.DeleteAll();
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