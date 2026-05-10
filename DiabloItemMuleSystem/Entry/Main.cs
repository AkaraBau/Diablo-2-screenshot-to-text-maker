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
            string filePath = null;
            var allStatTypes = Enum.GetValues<StatType>();  

            List <Item> allItems = Utils.Initiation(args);
            List<string> sItems = Utils.ItemToString(allItems);


            Console.WriteLine("[Commands]");
            var allUserActions = Enum.GetValues<UserAction>();
            foreach (var g in allUserActions)
            {
                Console.WriteLine(g.ToString());
            }
            
            while (true)
            {
                
                if (!UserAction.TryParse(Console.ReadLine(), out UserAction result))
                {
                    Console.WriteLine("wrong input try again");
                }

                switch (result)
                {
                    case UserAction.Print:

                        Utils.PrintList(allItems);
                        break;

                    case UserAction.CreateTxt:

                        Utils.ExportItemsToTxt(allItems);
                        break;

                    case UserAction.OrderByStat:

                        StatType sortCall = UserUtils.GetStat();
                        allItems.Sort(new SortByStat(sortCall));
                        break;

                    case UserAction.Ocr:

                        allItems = Ocr.SingleScan(allItems); ;
                        break;

                    case UserAction.OcrAll:

                        filePath = UserUtils.GetFilePath("");
                        var mergeList = Ocr.MultiScan(filePath);
                        allItems.AddRange(mergeList);
                        break;

                    case UserAction.ParseTxt:

                        filePath = UserUtils.GetFilePath(".txt");
                        string txtFile = File.ReadAllText(filePath);
                        var mergelist = Utils.TxtFileToListItem(txtFile);
                        allItems.AddRange(mergelist);
                        break;

                    case UserAction.GenericItemSort:

                        allItems.Sort(new GenericItemSort(allStatTypes));
                        Console.WriteLine("Sorted");
                        break;

                    case UserAction.SearchByStats:

                        int howManyStats = UserUtils.GetNumber("amount of stats");
                        int top = 0;
                        int bottom = 0;

                        List<Item> searchedList = new List<Item>();


                        for (int i = 0; i < howManyStats; i++)
                        {
                            StatType statForSearch = UserUtils.GetStat();
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

                        searchedList.Sort(new GenericItemSort(allStatTypes));
                        Utils.PrintList(searchedList);

                        if (searchedList.Count == 0)
                        {
                            Console.WriteLine("No items with those stats");
                        }
                        break;

                    case UserAction.RemoveById:

                        int remove = UserUtils.GetNumber("Id");
                        allItems.RemoveAll(item => item.Id == remove);
                        break;

                    case UserAction.GetAllFromDatabase:

                        allItems = Database.GetItems();
                        break;

                    case UserAction.AddAllToDatabase:

                        foreach (var item in allItems)
                        {
                            Database.AddItem(item);
                        }
                        break;

                    case UserAction.DeleteAllFromDatabase:

                        Database.DeleteAll();
                        break;

                    case UserAction.Quit:
                        return;
                        

                }

            }
        }
    }
}