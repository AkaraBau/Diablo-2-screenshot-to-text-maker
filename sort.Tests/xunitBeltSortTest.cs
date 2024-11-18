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
            string[] itemSortParameters = new string[] { "FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG","@RES", "PR", "LR", "FR", "PLR", "ED", "GOLD" };

            // belts for testing
            
            List<string> item1 = new List<string> { "SB"," ",  "7FCR" };
            List<string> item2 = new List<string> { "SB"," ", "30STR" };
            List<string> item3 = new List<string> { "SB", " ", "7FCR" };
            List<string> item4 = new List<string> { "SB",  " ", "10FCR" };
            List<string> item5 = new List<string> { "SB",  " ", "10FCR", "3MANA" };
            List<string> item6 = new List<string> { "SB", " ", "10FCR", "10MANA" };
            List<string> item7 = new List<string> {  "SB",  " ", "29STR" };
            List<string> item8 = new List<string> {  "SB", " ", "10FCR", "10MANA" };
            List<string> item9 = new List<string> { "SB", " ", "10FCR", "3MANA" };
            List<string> item10 = new List<string> { "SB", " ", "10FCR" };
            
            //list before sort
            List<Item> Actual = new List<Item> {       new Item(item1), 
                                                       new Item(item2), 
                                                       new Item(item3), 
                                                       new Item(item4), 
                                                       new Item(item5), 
                                                       new Item(item6), 
                                                       new Item(item7), 
                                                       new Item(item8), 
                                                       new Item(item9), 
                                                       new Item(item10) 
                                                        };
            //expected result of the sort
            List<Item> Expected = new List<Item> {       new Item(item6), // 10fcr 10m
                                                         new Item(item8), // 10fcr 10m
                                                         new Item(item5), // 10fcr 3m
                                                         new Item(item9), // 10fcr 3m 
                                                         new Item(item4), //10fcr
                                                         new Item(item10), //10fcr 
                                                         new Item(item1), //7fcr
                                                         new Item(item3), //7fcr
                                                         new Item(item2), //30str
                                                         new Item(item7) //29str                                                        
                                                          }; 
            //running the method
            Actual.Sort(new GenericItemSort(itemSortParameters));

            Assert.Equal(Expected, Actual); 
        }
    }
}