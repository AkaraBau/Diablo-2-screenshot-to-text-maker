using DiabloItemMuleSystem.Models;
using DiabloItemMuleSystem.Services;
using DiabloItemMuleSystem.Utilities;
using System.Collections.Generic;
using System;

namespace DiabloItemMuleSystem.Entry
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string filePath = null;
            var allStatTypes = Enum.GetValues<StatType>();


            List<Item> allItems = Utils.Initiation(args);
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

                        Utils.PromptAndExportItemsToTxt(allItems);
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

                        allItems = Utils.PromptAndImportItemsFromTxtFile(allItems);
                        break;

                    case UserAction.GenericItemSort:

                        allItems.Sort(new GenericItemSort(allStatTypes));
                        Console.WriteLine("Sorted");
                        break;

                    case UserAction.SearchByStats:

                        List<Item> searchedList = new List<Item>();
                        searchedList = Utils.PromptAndSearchByStats(allItems);
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