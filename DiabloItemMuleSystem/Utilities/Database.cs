using System;
using System.Collections.Generic;
using System.Linq;
using DiabloItemMuleSystem.Data;
using DiabloItemMuleSystem.Models;


namespace DiabloItemMuleSystem.Utilities
{
    public class Database 
    {
        public static void AddItem(Item item)
        {
            using (var itemContext = new ItemDbContext())
            {

                itemContext.Items.Add(item);

                foreach (var stats in item.ListOfStats)
                {
                    itemContext.Stats.Add(stats);
                }

                itemContext.SaveChanges();
            }
        }
        public static void DeleteAll()
        {
            using (var itemContext = new ItemDbContext())
            {
                itemContext.Items.RemoveRange(itemContext.Items);
                itemContext.Stats.RemoveRange(itemContext.Stats);
                itemContext.SaveChanges();
            }
        }
        public static int GetHighestId(string type)
        {


            ItemDbContext ItemContext = new ItemDbContext();

            if (type == "Item")
            {
                if (ItemContext.Items.Count() > 0)
                {
                    return ItemContext.Items.Max(item => item.Id);
                }
                else return 0;

            }
            else if (type == "Stats")
            {
                if (ItemContext.Stats.Count() > 0)
                {
                    return ItemContext.Stats.Max(stats => stats.StatsId);
                }
                else return 0;
            }
            else
                return 0;


        }
        public static List<Stats> GetStats(int ID)
        {

            ItemDbContext ItemContext = new ItemDbContext();

            return ItemContext.Stats.Where(s => s.ItemId == ID).ToList();
        }
        public static List<Item> GetItems()
        {
            List<Item> itemList = new List<Item>();
            ItemDbContext itemContext = new ItemDbContext();

            foreach (var i in itemContext.Items)
            {
                List<Stats> stats = Database.GetStats(i.Id);
                Item item = new Item(i, stats);
                itemList.Add(item);
            }

            return itemList;
        }
    }

}

