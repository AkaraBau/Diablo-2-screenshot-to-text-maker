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
            var input = @"C:\Users\fide_\Desktop\Programmering\Tessaract\";
            string[] massInput = Utils.DetectFiles(input);
            string[] massOutput = new string[massInput.Length];
            string[] splitData = new string[14];
            List<string> sBelt = new List<string>();
            List<Belt> allBelts = new List<Belt>(); // creation of a list

            Console.WriteLine("Would you like to change directory for the ocr scan? y/n");
            String call = Console.ReadLine();
            if (call == "y")
            {
                Console.WriteLine("Your directory should look like this " + input);
                input = Console.ReadLine();
                Console.WriteLine("scan starting");
            }
            else if (call == "n")
            {
                Console.WriteLine("Scan starting.");
            }

            for (int i = 0; i < massInput.Length; i++)
            {
                using (var stream = Tesseract.ImageToTxt(massInput[i], languages: new[] { Language.English, Language.French }))
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string

                    massOutput[i] = reader.ReadToEnd(); //making stream -> string []
                    massOutput[i] = Utils.ChangeLetters(massOutput[i]); //shorten method
                    massOutput[i] = Utils.ShortenString(massOutput[i]); //shorten method
                    splitData = massOutput[i].Split(new[] { '\n' }, StringSplitOptions.None); //splitting string into string []
                    splitData = splitData.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace
                    Belt belt = new Belt(splitData);
                    allBelts.Add(belt);
                }
            }

            Console.WriteLine("What would you like to do?");
            Console.WriteLine("print[p], txt[t],sort(s) quit(q)");
            call = null;
            while (call != "q")
            {
                call = Console.ReadLine();
                if (call == "p")
                {
                    Utils.PrintList(allBelts);
                }
                else if (call == "t")
                {
                    sBelt = Utils.ObjectToString(allBelts);
                    input = Path.Combine(input, "belts.txt");
                    File.WriteAllLines(input, sBelt);
                }
                else if (call == "s")
                {
                sBelt.Sort((a,b)=>a.CompareTo(b));
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