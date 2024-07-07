using System;
using System.Collections.Generic;
using System.IO;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public class Belt
    {
        public string name {get; set;}
        public string type {get; set;}
        public string defense {get; set;}
        public string size {get; set;}
        public string durability {get; set;}
        public string req1 {get; set;}
        public string req2 {get; set;}
        public string craft1 {get; set;}
        public string craft2 {get; set;}
        public string craft3 {get; set;}
        public string suffix1 {get; set;}
        public string suffix2 {get; set;}
        public string suffix3 {get; set;}
        public string suffix4 {get; set;} 
    }
}