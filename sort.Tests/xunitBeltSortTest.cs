using Programming;
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
            string[] beltSortParameters = new string[] { "FCR", "FHR", "STR", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "DPL", "QDPL", "LIGHTRADIUS", "MS", "ATDO", "GOLD" };

            // belts for testing
            
            string[] Belt2 = new string[] { " ", "SB", " ", " ", " ", " ", " ",  "7FCR" };
            string[] Belt3 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "30STR" };
            string[] Belt4 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "7FCR" };
            string[] Belt5 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "10FCR" };
            string[] Belt6 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "10FCR", "3MANA" };
            string[] Belt7 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "10FCR", "10MANA" };
            string[] Belt8 = new string[] { "", "SB", " ", " ", " ", " ", " ", "29STR" };
            string[] Belt9 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "10FCR", "10MANA" };
            string[] Belt10 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "10FCR", "3MANA" };
            string[] Belt11 = new string[] { " ", "SB", " ", " ", " ", " ", " ", "10FCR" };
            
            //list before sort
            List<Item> Actual = new List<Item> {       new Item(Belt2), 
                                                       new Item(Belt3), 
                                                       new Item(Belt4), 
                                                       new Item(Belt5), 
                                                       new Item(Belt6), 
                                                       new Item(Belt7), 
                                                       new Item(Belt8), 
                                                       new Item(Belt9), 
                                                       new Item(Belt10), 
                                                       new Item(Belt11) 
                                                        };
            //expected result of the sort
            List<Item> Expected = new List<Item> {       new Item(Belt7), // 10fcr 10m
                                                         new Item(Belt9), // 10fcr 10m
                                                         new Item(Belt6), // 10fcr 3m
                                                         new Item(Belt10), // 10fcr 3m 
                                                         new Item(Belt5), //10fcr
                                                         new Item(Belt11), //10fcr 
                                                         new Item(Belt2), //7fcr
                                                         new Item(Belt4), //7fcr
                                                         new Item(Belt3), //30str
                                                         new Item(Belt8) //29str                                                        
                                                          }; 
            //running the method
            Actual.Sort(new GenericBeltSort(beltSortParameters));

            Assert.Equal(Expected, Actual); 
        }
    }
}