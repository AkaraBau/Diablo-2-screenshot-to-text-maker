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
    public class JPGtoTXT
    {
        public static void DoIt()
        {
            var input = @"C:\Users\fide_\Desktop\Programmering\Tessaract\924LM1.png"; 
            var output = input.Replace(".png", ".txt");
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            using (var writer = File.OpenWrite(output))
            {
                stream.CopyTo(writer);
            }
            Console.ReadLine();
        }
    }
}