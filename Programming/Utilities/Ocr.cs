using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; //accessing case sensitive check
using TesseractSharp;
using System.Text.RegularExpressions;
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
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string

                item = reader.ReadToEnd(); //making stream -> string 
                item = Utils.RemoveAllWhiteSpace(item);
                item = Utils.ChangeLetters(item);
                item = Utils.ShortenString(item);
                data = item.Split(new[] { '\n' }, StringSplitOptions.None); //split string into array of strings 
                data = data.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace

                List<string> listData = new List<string>(data);
                listData = Utils.RemoveListContentBeforeObjectCreationOcr(listData);
                Console.WriteLine("item added");
                return listData;
            }
        }
        public static List<Item> MultiScan(string filePath)
        {
            List<Item> itemList = new List<Item>();
            var stringList = new List<string>(); //list of <String>
            string[] splitData = new string[14];
            string[] massInput = Utils.DetectFiles(filePath);
            string[] massOutput = new string[massInput.Length]; //array of strings containing output from tessaract 

            for (int i = 0; i < massInput.Length; i++)
            {
                using (var stream = Tesseract.ImageToTxt(massInput[i], languages: new[] { Language.English, Language.French }))
                {

                    //loading bar
                    Utils.ProgressBar(i + 1, massInput.Length, 10);
                    //ocr function
                    StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8); //making stream -> string
                    massOutput[i] = reader.ReadToEnd(); //making stream -> string []

                    massOutput[i] = Utils.RemoveAllWhiteSpace(massOutput[i]); // remove all whitespace
                    massOutput[i] = Utils.ChangeLetters(massOutput[i]); //formatting, change of broken reading of letters
                    massOutput[i] = Utils.ShortenString(massOutput[i]); //reformatting. ex "STRENGTH" to "STR" 

                    splitData = massOutput[i].Split(new[] { '\n' }, StringSplitOptions.None); //splitting string into string []
                    splitData = splitData.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(); // removing whitespace

                    List<string> listSplitData = new List<string>(splitData);
                    listSplitData = Utils.RemoveListContentBeforeObjectCreationOcr(listSplitData); //removing unecessary data before creating object. 

                    Item belt = new Item(listSplitData); //Creation of belt
                    itemList.Add(belt); //adding belt to list 
                }
            }
            Console.WriteLine("\nDone.");
            return itemList;
        }
    }


}
