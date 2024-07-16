using System;
using System.Collections.Generic;
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
            List<Belt> allBelts = new List<Belt>(); // creation of a list

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
            Utils.PrintList(allBelts);

            Console.ReadLine();
        }
    }
}