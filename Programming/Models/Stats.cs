using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;

namespace DiabloItemMuleSystem
{
    public class Stats
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public int? Amount { get; set; }

        public Stats(int ID ,string data)
        {
            Id = ID;
            Amount = Utils.ExtractIntFromString(data);
            Name = Utils.RemoveNumbers(data);

        }
        public override string ToString()
        {
            string result = $"{Amount}{Name}";
            return result;
        }

        public override bool Equals(object obj)
        {
            Stats other = obj as Stats;

            if (other == null) return false;

            else if (this.Name == other.Name && this.Amount == other.Amount)
            {
                return true;
            }

            else return false; 
             

        }
        public override int GetHashCode() 
            { 
            return this.Amount.GetHashCode() ^ this.Name.GetHashCode(); 
        }   
    }
}