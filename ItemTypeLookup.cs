using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Formats.Asn1;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.XPath;
using Microsoft.VisualBasic;
using NLog.LayoutRenderers;
using TesseractSharp;
using TesseractSharp.Hocr;

namespace AkarasDegenStuff
{
    public static class ItemTypeLookup
    {
        private static readonly Dictionary<string, ItemType> _ConvertItemName = new Dictionary<string, ItemType>{
                     //Belts
                     {"SB", ItemType.Belt},
                     {"VB", ItemType.Belt},
                     {"LIGHTBELT", ItemType.Belt},

                     {"SPS", ItemType.Belt},
                     {"DHS", ItemType.Belt},
                     {"DHS", ItemType.Belt},

                     {"MC", ItemType.Belt},
                     {"MESHBELT", ItemType.Belt},
                     {"SASH", ItemType.Belt},

                     {"TROLLBELT", ItemType.Belt},
                     {"BATTLEBELT", ItemType.Belt},
                     {"HEAVYBELT", ItemType.Belt},

                     {"COLOSSUSGIRDLE", ItemType.Belt},
                     {"WARBELT", ItemType.Belt},
                     {"PLATEDBELT", ItemType.Belt},
                     
                     //jewelry
                     {"AMULET", ItemType.Amulet},
                     {"RING", ItemType.Ring},
                     //boots
                     {"WYRMHIDEBOOTS", ItemType.Boots},
                     {"DEMONHIDEBOOTS", ItemType.Boots},
                     {"BOOTS", ItemType.Boots},
                     
                     {"SCARABSHELLBOOTS", ItemType.Boots},
                     {"HEAVYBOOTS", ItemType.Boots},
                     {"SHARKSKINBOOTS", ItemType.Boots},
                     
                     {"BONEWEAVEBOOTS", ItemType.Boots},
                     {"MESHBOOTS", ItemType.Boots},
                     {"CHAINBOOTS", ItemType.Boots},
                     
                     {"MIRROREDBOOTS", ItemType.Boots},
                     {"BATTLEBOOTS", ItemType.Boots},
                     {"LIGHTPLATEDBOOTS", ItemType.Boots},
                     
                     {"MYRMIDONGREAVES", ItemType.Boots},
                     {"WARBOOTS", ItemType.Boots},
                     {"GREAVES", ItemType.Boots},
                     //gloves
                     {"BRAMBLEMITTS", ItemType.Gloves},
                     {"DEMONHIDEGLOVES", ItemType.Gloves},
                     {"LEATHERGLOVES", ItemType.Gloves},

                     {"VAMPIREBONEGLOVES", ItemType.Gloves},
                     {"SHARKSKINGLOVES", ItemType.Gloves},
                     {"HEAVYGLOVES", ItemType.Gloves},

                     {"VAMBRACES", ItemType.Gloves},
                     {"HEAVYBRACERS", ItemType.Gloves},
                     {"CHAINGLOVES", ItemType.Gloves},

                     {"CRUSADERGAUNTLETS", ItemType.Gloves},
                     {"BATTLEGAUNTLETS", ItemType.Gloves},
                     {"LIGHTGAUNTLETS", ItemType.Gloves},

                     {"OGREGAUNTLETS", ItemType.Gloves},
                     {"WARGAUNTLETS", ItemType.Gloves},
                     {"GAUNTLETS", ItemType.Gloves},
                     //armor 
                     /*{"SACREDARMOR", ItemType.Armor},
                     {"ANCIENTARMOR", ItemType.Armor},
                     {"ORNATEPLATE", ItemType.Armor},

                     {"WYRMHIDE", ItemType.Armor},
                     {"SERPENTSKINARMOR", ItemType.Armor},
                     {"LEATHERARMOR", ItemType.Armor},

                     {"SHADOWPLATE", ItemType.Armor},
                     {"FULLPLATEMAIL", ItemType.Armor},
                     {"CHAOSARMOR", ItemType.Armor},

                     {"GREATHAUBERK", ItemType.Armor},
                     {"CUIRASS", ItemType.Armor},
                     {"BREASTPLATE", ItemType.Armor},*/



        };


        public static ItemType GetTypeFromSubtype(string input)
        {
           var output = _ConvertItemName[input];
             

           return output;
        }
    }
}
