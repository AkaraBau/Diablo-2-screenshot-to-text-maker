using DiabloItemMuleSystem;
using sort.Tests; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Xunit;

namespace sort.Tests
{
    public class SortMethodTests
    {

        [Fact]
        public void GenericSortMethodTest()
        {
            string[] itemSortParameters = new string[] { "FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG","@RES", "PR", "LR", "FR", "PLR", "ED", "DPL", "QDPL", "LIGHTRADIUS", "MS", "ATDO", "GOLD" };

            // belts for testing
            
            List<string> Item1 = new List<string> { "SB"," ",  "7FCR" };
            List<string> Item2 = new List<string> { "SB"," ", "30STR" };
            List<string> Item3 = new List<string> { "SB", " ", "7FCR" };
            List<string> Item4 = new List<string> { "SB",  " ", "10FCR" };
            List<string> Item5 = new List<string> { "SB",  " ", "10FCR", "3MANA" };
            List<string> Item6 = new List<string> { "SB", " ", "10FCR", "10MANA" };
            List<string> Item7 = new List<string> {  "SB",  " ", "29STR" };
            List<string> Item8 = new List<string> {  "SB", " ", "10FCR", "10MANA" };
            List<string> Item9 = new List<string> { "SB", " ", "10FCR", "3MANA" };
            List<string> Item10 = new List<string> { "SB", " ", "10FCR" };
            
            //list before sort
            List<Item> Actual = new List<Item> {       new Item(Item1), 
                                                       new Item(Item2), 
                                                       new Item(Item3), 
                                                       new Item(Item4), 
                                                       new Item(Item5), 
                                                       new Item(Item6), 
                                                       new Item(Item7), 
                                                       new Item(Item8), 
                                                       new Item(Item9), 
                                                       new Item(Item10) 
                                                        };
            //expected result of the sort
            List<Item> Expected = new List<Item> {       new Item(Item6), // 10fcr 10m
                                                         new Item(Item8), // 10fcr 10m
                                                         new Item(Item5), // 10fcr 3m
                                                         new Item(Item9), // 10fcr 3m 
                                                         new Item(Item4), //10fcr
                                                         new Item(Item10), //10fcr 
                                                         new Item(Item1), //7fcr
                                                         new Item(Item3), //7fcr
                                                         new Item(Item2), //30str
                                                         new Item(Item7) //29str                                                        
                                                          }; 
            //running the method
            Actual.Sort(new GenericItemSort(itemSortParameters));

            Assert.Equal(Expected, Actual); 
        }
    }
}