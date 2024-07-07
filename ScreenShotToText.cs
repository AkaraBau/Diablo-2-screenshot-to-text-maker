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
            var input = @"C:\Users\fide_\Desktop\d2 items/677.png";

            string item = null; 
            using (var stream = Tesseract.ImageToTxt(input, languages: new[] { Language.English, Language.French }))
            {
             StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
             item = reader.ReadToEnd();
             Console.Write(item);
            }
            Console.ReadLine();
        }
    }
}