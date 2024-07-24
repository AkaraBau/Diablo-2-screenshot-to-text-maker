using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq; //accessing case sensitive check
using System.Runtime.InteropServices;
using System.Xml.XPath;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Core;
using TesseractSharp.Hocr;
using System.Text.RegularExpressions;

namespace AkarasDegenStuff
{
    public enum ItemName
    {
     Belt,
     Ring,
     Amulet,
     Jewel,
     Armor,
     Weapon,
     Circlet,
     Coronet,
     Diadem,
     Shield, 
     Boots,
     Gloves,
     Helm
    }
}
