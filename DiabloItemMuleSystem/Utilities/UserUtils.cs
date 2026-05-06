using NLog.Targets;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace DiabloItemMuleSystem.Utilities
{
    public static class UserUtils
    {
        public static string GetFilePath(string Type)
        {
            string filePath = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new";

            Console.WriteLine("Pick a directory: \n");
            Console.WriteLine("Format: " + filePath + Type);

            filePath = Console.ReadLine();

            return filePath;
        }
        public static int GetNumber(string args)
        {
            Console.WriteLine("Which " + args + "?");

            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }

                Console.WriteLine("Invalid input, try again.");
            }
        }
        public static string GetStat()
        {
            string[] statNamesItems = new string[] { "FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "GOLD" };
            Console.WriteLine("Which stat?"); 
            while (true)
            {
                var result = Console.ReadLine();
                foreach(var s in statNamesItems)
                {
                    if (s == result)
                    return result; 
                }
                Console.WriteLine("Invalid input. Valid inputs:");
                foreach (var s in statNamesItems)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }
}
