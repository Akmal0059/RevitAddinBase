using AdWin = Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace RevitAddinBase.RevitControls
{
    public class Textbox : RibbonItemBase
    {
        public string HintText { get; set; }
        public double TextboxWidth { get; set; }
        public string IconPath { get; set; }

        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            throw new NotImplementedException();
        }

        public override RibbonItemData GetData(Dictionary<string, object> resources)
        {
            string name = CommandName;
            TextBoxData tbData = new TextBoxData(name);
            return tbData;
        }
    }
}
