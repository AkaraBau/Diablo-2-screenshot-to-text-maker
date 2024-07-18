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
            var input = @"C:\Users\fide_\Desktop\d2 items\Crafted\caster belts\Have\new\Cbelt-B";
            string[] massInput = Utils.DetectFiles(input); //array of strings containing input to tessaract.
            string[] splitData = new string[14]; // array of strings. Array which goes into the class Belt
            List<string> sBelt = new List<string>(); //list of <String>
            List<Belt> allBelts = new List<Belt>(); // list of <Belt>
            
            Console.WriteLine("Would you like to change directory for the ocr scan? y/n");

            String call = Console.ReadLine();
            
            if (call == "y")
            {
                Console.WriteLine("Your directory should look like this " + input);
                input = Console.ReadLine();
                massInput = Utils.DetectFiles(input); 
                Console.WriteLine("Reading the files.");
            }
            else if (call == "n")
            {
                Console.WriteLine("Reading the files.");
            }
            else
            {
                Console.WriteLine("Wrong input try again.");
                call = Console.ReadLine();
            }

            string[] massOutput = new string[massInput.Length]; //array of strings containing output from tessaract 

            for (int i = 0; i < massInput.Length; i++)
            {
                using (var stream = Tesseract.ImageToTxt(massInput[i], languages: new[] { Language.English, Language.French }))
                {
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string

                    massOutput[i] = reader.ReadToEnd(); //making stream -> string []
                    massOutput[i] = Utils.RemoveAllWhiteSpace(massOutput[i]); // remove all whitespace
                    massOutput[i] = Utils.ChangeLetters(massOutput[i]); //shorten method
                    massOutput[i] = Utils.ShortenString(massOutput[i]); //shorten method
                    splitData = massOutput[i].Split(new[] { '\n' }, StringSplitOptions.None); //splitting string into string []
                    splitData = splitData.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace
                    Belt belt = new Belt(splitData); //Creation of belt
                    allBelts.Add(belt); //adding belt to list 
                }
            }
            sBelt = Utils.ObjectToString(allBelts); // convert <object> to string
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("print[p], txt[t],sort(s) quit(q)");
            call = null;
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
                    input = Path.Combine(input, name + ".txt");
                    File.WriteAllLines(input, sBelt);
                }
                else if (call == "s")
                {
                sBelt.Sort();
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