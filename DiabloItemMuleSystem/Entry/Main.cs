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

            string[] sortParameters = ["FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "GOLD"]; //I should probably not be repeating this at many places in my code FKN TODO

            List<Item> allItems = Utils.Initiation(args);
            List<string> sItems = Utils.ItemToString(allItems);


            Console.WriteLine("[Commands]");
            var getAllEnums = Enum.GetValues<UserAction>();
            foreach (var g in getAllEnums)
            {
                Console.WriteLine(g.ToString());
            }
            
            while (true)
            {
                string filePath = null;
                if (UserAction.TryParse(Console.ReadLine(), out UserAction result))
                {
                    if (result == UserAction.Print) 
                    {
                        Utils.PrintList(allItems);
                    }
                    else if (result == UserAction.CreateTxt)
                    {
                        Console.WriteLine("What would you like to name the file?");
                        string name = Console.ReadLine();
                        filePath = UserUtils.GetFilePath("");
                        filePath = Path.Combine(filePath, name + ".txt");
                        sItems = Utils.ItemToString(allItems);
                        File.WriteAllLines(filePath, sItems);

                        Console.WriteLine("Txt file created");
                    }
                    else if (result == UserAction.OrderByStat)
                    {
                        string sortCall = UserUtils.GetStat();
                        allItems.Sort(new SortByStat(sortCall));
                    }
                    else if (result == UserAction.Ocr)
                    {
                        filePath = UserUtils.GetFilePath(".png");
                        Item item = new Item(Ocr.SingleScan(filePath));
                        allItems.Add(item);
                    }
                    else if (result == UserAction.OcrAll)
                    {

                        filePath = UserUtils.GetFilePath("");
                        var mergeList = Ocr.MultiScan(filePath);
                        allItems.AddRange(mergeList);

                    }
                    else if (result == UserAction.ParseTxt)
                    {
                        filePath = UserUtils.GetFilePath(".txt");
                        string txtFile = File.ReadAllText(filePath);
                        var mergelist = Utils.TxtFileToListItem(txtFile);
                        allItems.AddRange(mergelist);
                    }
                    else if (result == UserAction.GenericItemSort)
                    {
                        allItems.Sort(new GenericItemSort(sortParameters));
                        Console.WriteLine("Sorted");
                    }
                    else if (result == UserAction.SearchByStats)   // TODO still think this looks ugly 
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
                    else if (result == UserAction.RemoveById)
                    {

                        int remove = UserUtils.GetNumber("Id");
                        allItems.RemoveAll(item => item.Id == remove);

                    }
                    else if (result == UserAction.GetAllFromDatabase)
                    {
                        allItems = Database.GetItems();

                    }
                    else if (result == UserAction.AddAllToDatabase)
                    {
                        foreach (var item in allItems)
                        {
                            Database.AddItem(item);
                        }
                    }
                    else if (result == UserAction.DeleteAllFromDatabase)
                    {
                        Database.DeleteAll();
                    }
                    else if (result == UserAction.Quit)
                    {
                        return;
                    }

                }
                else
                {
                    Console.WriteLine("wrong input try again");
                }

            }
        }
    }
}