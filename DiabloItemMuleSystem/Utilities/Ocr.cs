using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; //accessing case sensitive check
using TesseractSharp;
using DiabloItemMuleSystem.Models;


namespace DiabloItemMuleSystem.Utilities
{
    public class Ocr 
    {
        public static List<string> SingleScan(string filePath)
        {
            string item = null;
            string[] data = new string[14];
            using (var stream = Tesseract.ImageToTxt(filePath, languages: new[] { Language.English, Language.French }))
            {
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);

                item = reader.ReadToEnd(); 
                item = StringUtils.RemoveAllWhiteSpace(item);
                item = StringUtils.ChangeLetters(item);
                item = StringUtils.ShortenString(item);
                data = item.Split(new[] { '\n' }, StringSplitOptions.None); 
                data = data.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); 

                List<string> listData = new List<string>(data);
                listData = Utils.RemoveListContentBeforeObjectCreationOcr(listData);
                Console.WriteLine("item added");
                return listData;
            }
        }
        public static List<Item> MultiScan(string filePath)
        {
            List<Item> itemList = new List<Item>();
            var stringList = new List<string>(); 
            string[] splitData = new string[14];
            string[] massInput = Utils.DetectFiles(filePath);
            string[] massOutput = new string[massInput.Length]; 

            for (int i = 0; i < massInput.Length; i++)
            {
                using (var stream = Tesseract.ImageToTxt(massInput[i], languages: new[] { Language.English, Language.French }))
                {

                    
                    Utils.StatusOcr(i + 1, massInput.Length);
                    
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); 
                    massOutput[i] = reader.ReadToEnd();

                    massOutput[i] = StringUtils.RemoveAllWhiteSpace(massOutput[i]);
                    massOutput[i] = StringUtils.ChangeLetters(massOutput[i]); 
                    massOutput[i] = StringUtils.ShortenString(massOutput[i]); 
                    splitData = massOutput[i].Split(new[] { '\n' }, StringSplitOptions.None); //splitting string into string []
                    splitData = splitData.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace again due to rare occasion where all whitespace wouldnt get removed by regex

                    List<string> listSplitData = new List<string>(splitData);
                    listSplitData = Utils.RemoveListContentBeforeObjectCreationOcr(listSplitData); //removing unecessary data before creating object. (matching parsing and ocr) 

                    Item item = new Item(listSplitData);
                    itemList.Add(item);  
                }
            }
            Console.WriteLine("\nDone.");
            return itemList;
        }
    }


}
