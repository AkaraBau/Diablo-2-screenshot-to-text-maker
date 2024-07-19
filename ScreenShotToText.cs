using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class ScreenShotToText
    {
        public static void DoIt()
        {
            
            
            var input = @"C:\Users\fide_\Desktop\Programmering\Tessaract\";
            
            string item = null;
            string[]data = new string[14];
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string

                item = reader.ReadToEnd(); //making stream -> string 
                item = Utils.RemoveAllWhiteSpace(item);
                item = Utils.ChangeLetters(item); 
                item = Utils.ShortenString(item);
                data = item.Split(new[] { '\n' }, StringSplitOptions.None); //split string into array of strings 
                data = data.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace
                Belt belt = new Belt(data); // creation of a Belt
            }

            

            


            Console.ReadLine();
        }
    }
}