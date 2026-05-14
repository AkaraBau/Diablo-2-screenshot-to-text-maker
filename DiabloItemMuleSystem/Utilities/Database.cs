using System;
using System.Collections.Generic;
using System.Linq;
using DiabloItemMuleSystem.Data;
using DiabloItemMuleSystem.Models;
using Microsoft.EntityFrameworkCore;


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
                itemContext.Database.ExecuteSqlRaw("DELETE FROM stats");
                itemContext.Database.ExecuteSqlRaw("DELETE FROM items");

                itemContext.SaveChanges();
            }
        }
        public static List<Stats> GetStatsById(Guid ID)
        {

            using (ItemDbContext ItemContext = new ItemDbContext())
            {
                return ItemContext.Stats.Where(s => s.ItemId == ID).ToList();
            }
        }
        public static List<Item> GetItems()
        {
            List<Item> itemList = new List<Item>();
            using (ItemDbContext itemContext = new ItemDbContext())
            {
                foreach (var i in itemContext.Items)
                {
                    List<Stats> stats = Database.GetStatsById(i.Id);
                    Item item = new Item(i, stats);
                    itemList.Add(item);
                }

                return itemList;
            }
        }
    }

}

