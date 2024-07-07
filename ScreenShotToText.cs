using System;
using System.IO;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class ScreenShotToText
    {
        public static void DoIt()
        {
            var input = @"C:\Users\fide_\Desktop\d2 items/6sl1.jpg";

            
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            {
                Console.Write(stream.ToString()); 
            }

            
            Console.ReadLine();
        }

    }
}