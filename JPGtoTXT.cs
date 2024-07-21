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

namespace AkarasDegenStuff
{
    public class JPGtoTXT
    {
        public static void DoIt()
        {
            var input = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new";
            var sBelt = new List<string>(); //list of <String>
            var allBelts = new List<Belt>(); //list of <Belt>
            
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("print[p], txt[t],sort[s], sortby[sb] add[a], remove[r], add multiple[am], quit[q]");
            String call = null;
            while (call != "q")
            {
                call = Console.ReadLine();
                if (call == "p")
                {
                    Utils.PrintList(sBelt);
                }
                else if (call == "t")
                {
                    Console.WriteLine("What would you like to name the file?");
                    String name = Console.ReadLine();

                    Console.WriteLine("What directory should the file get created in?"); 
                    Console.WriteLine("Format:" + @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have"); 
                    input = Console.ReadLine(); 

                    input = Path.Combine(input, name + ".txt");
                    File.WriteAllLines(input, sBelt);
                }
                else if (call == "s")
                {
                    sBelt.Sort();
                }
                else if (call == "sb") 
                {
                    Console.WriteLine("What keyword do you want to sort by? ex FHR or STR"); 
                    string input1 = Console.ReadLine();
                    sBelt = Utils.SortBy(sBelt, input1);
                }
                else if (call == "a")
                {
                    Console.WriteLine("What image would you like to add?\n" + input + @"sln.PNG");
                    input = Console.ReadLine();

                    Belt belt = new Belt(Utils.SingleBeltOcr(input));
                    allBelts.Add(belt); 
                    var mergeList = Utils.ObjectToString(allBelts);
                    sBelt.AddRange(mergeList);
                }
                else if (call == "am")
                {
                    Console.WriteLine("What directory would you like to scan? Format below \n" + input);
                    input = Console.ReadLine();

                    var mergeList = Utils.MultiBeltOcr(input); //multi scan method
                    sBelt.AddRange(mergeList); //adding output to list
                }
                else if (call == "r")
                {
                    Console.WriteLine("Which belt would you like to remove?");
                    Console.WriteLine("Format for input: SB1ASLMM/SB/34DEF/63LREQ	10FCR/16ADDSCOLDDAMAGE/0STR/581LIFE/38MANA/4MREG");
                    string remove = Console.ReadLine();
                    sBelt.Remove(remove);
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