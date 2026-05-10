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
                string filePath = null;
                if (!UserAction.TryParse(Console.ReadLine(), out UserAction result))
                {
                    Console.WriteLine("wrong input try again");
                }
                
                if (result == UserAction.Print)
                {
                    Utils.PrintList(allItems);
                }
                else if (result == UserAction.CreateTxt)
                {
                    Utils.ExportItemsToTxt(allItems); 
                }
                else if (result == UserAction.OrderByStat)
                {
                    StatType sortCall = UserUtils.GetStat();
                    allItems.Sort(new SortByStat(sortCall));
                }
                else if (result == UserAction.Ocr)
                {
                    allItems = Ocr.SingleScan(allItems);
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
                    allItems.Sort(new GenericItemSort(allStatTypes));
                    Console.WriteLine("Sorted");
                }
                else if (result == UserAction.SearchByStats)   // TODO still think this looks ugly 
                {
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
        }
    }
}