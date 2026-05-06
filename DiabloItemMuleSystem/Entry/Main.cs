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


            Console.WriteLine("[Commands]");
            var getAllEnums = Enum.GetValues<UserAction>();
            foreach (var g in getAllEnums)
            {
                Console.WriteLine(g.ToString());
            }
            string call = null;
            while (call != UserAction.Quit.ToString())
            {
                string filePath = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new";
                call = Console.ReadLine();
                if (call == UserAction.Print.ToString())
                {
                    Utils.PrintList(allItems);
                }
                else if (call == UserAction.CreateTxt.ToString())
                {
                    Console.WriteLine("What would you like to name the file?");
                    string name = Console.ReadLine();
                    filePath = UserUtils.GetFilePath(""); 
                    filePath = Path.Combine(filePath, name + ".txt");
                    sItems = Utils.ItemToString(allItems);
                    File.WriteAllLines(filePath, sItems);

                    Console.WriteLine("Txt file created");
                }
                else if (call == UserAction.OrderByStat.ToString())
                {
                    string sortCall = UserUtils.GetStat();
                    allItems.Sort(new SortByStat(sortCall));
                }
                else if (call == UserAction.Ocr.ToString())
                {
                    filePath = UserUtils.GetFilePath(".png"); 
                    Item item = new Item(Ocr.SingleScan(filePath));
                    allItems.Add(item);
                }
                else if (call == UserAction.OcrAll.ToString())
                {
                     
                    filePath = UserUtils.GetFilePath("");
                    var mergeList = Ocr.MultiScan(filePath);
                    allItems.AddRange(mergeList); 

                }
                else if (call == UserAction.ParseTxt.ToString())
                {
                    filePath = UserUtils.GetFilePath(".txt"); 
                    string txtFile = File.ReadAllText(filePath);
                    var mergelist = Utils.TxtFileToListItem(txtFile);
                    allItems.AddRange(mergelist);
                }
                else if (call == UserAction.GenericItemSort.ToString())
                {
                    allItems.Sort(new GenericItemSort(sortParameters));
                    Console.WriteLine("Sorted");
                }
                else if (call == UserAction.SearchByStats.ToString())   
                {
                    int howManyStats = UserUtils.GetNumber("amount of stats");
                    int top = 0;
                    int bottom = 0;
                    string statForSearch = null;
                    List<Item> searchedList = new List<Item>();


                    for (int i = 0; i < howManyStats; i++)
                    {
                        statForSearch = UserUtils.GetStat();
                        top = UserUtils.GetNumber("top range");
                        bottom = UserUtils.GetNumber("bottom range"); 

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
                else if (call == UserAction.RemoveById.ToString())
                {

                    int remove = UserUtils.GetNumber("Id"); 
                    allItems.RemoveAll(item => item.Id == remove);

                }
                else if (call == UserAction.GetAllFromDatabase.ToString())
                {
                    allItems = Database.GetItems(); 

                }
                else if (call == UserAction.AddAllToDatabase.ToString())
                {
                    foreach (var item in allItems)
                    {
                        Database.AddItem(item);
                    }
                }
                else if (call == UserAction.DeleteAllFromDatabase.ToString())
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