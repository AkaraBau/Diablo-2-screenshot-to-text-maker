using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;

namespace Programming
{
    public class Main
    {
        public static void DoIt()
        {
            string input = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new";
            List <string> sItems = new List<string>(); //list of <String>
            List<Item> allItems = new List<Item>(); //list of <Belt>
            string [] beltSortParameters = new string[] { "FCR", "FHR", "STR", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "DPL", "QDPL", "LIGHTRADIUS", "MS", "ATDO", "GOLD" };

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("print[p], txt[t], sortby[sb], generic belt sort[gbs], add[a], add multiple[am], quit[q]");
            String call = null;
            while (call != "q")
            {
                call = Console.ReadLine();
                if (call == "p")
                {
                    Utils.PrintList(sItems);
                }
                else if (call == "t")
                {
                    Console.WriteLine("What would you like to name the file?");
                    String name = Console.ReadLine();

                    Console.WriteLine("What directory should the file get created in?");
                    Console.WriteLine("Format:" + @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have");
                    input = Console.ReadLine();

                    input = Path.Combine(input, name + ".txt");
                    File.WriteAllLines(input, sItems);
                }
                else if (call == "sb")
                {
                    Console.WriteLine("What stat would you like to sort by? ");
                    var sortCall = Console.ReadLine().ToUpper().Trim();
                    allItems.Sort(new SortByStat(sortCall));
                    sItems = Utils.ItemToString(allItems);
                }
                else if (call == "a")
                {
                    Console.WriteLine("What image would you like to add?\n" + @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new\sln.PNG");
                    input = Console.ReadLine();

                    Item item = new Item(Utils.SingleBeltOcr(input));
                    allItems.Add(item);
                    sItems.Add(item.ToString());
                }
                else if (call == "am")
                {
                    Console.WriteLine("What directory would you like to scan? Format below \n" + input);
                    input = Console.ReadLine();

                    var mergeList = Utils.MultiBeltOcr(input); //multi scan method
                    allItems.AddRange(mergeList); //adding output to list
                    var mergeList2 = Utils.ItemToString(mergeList);
                    sItems.AddRange(mergeList2);
                }
                else if (call == "gbs")
                {
                    
                    allItems.Sort(new GenericBeltSort(beltSortParameters));
                    
                    sItems = Utils.ItemToString(allItems);

                    Console.WriteLine("Sorted");
                }
                else if (call == "q")
                {
                    return;
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