using System;
using System.Collections.Generic;
using System.IO;
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
            var input = @"C:\Users\fide_\Desktop\d2 items/677.png";
            string item = null; 
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            {
             StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
             item = reader.ReadToEnd();
             Console.Write(item); 
            }

            string[] splitItem = item.Split(new[] { '\n' }, StringSplitOptions.None);
            splitItem = splitItem.Where(x => !String.IsNullOrWhiteSpace(x)).ToArray();
            for (int i = 0; i < splitItem.Length-1; i++)
            {
                
            }

            Console.ReadLine();
        }
    }
}