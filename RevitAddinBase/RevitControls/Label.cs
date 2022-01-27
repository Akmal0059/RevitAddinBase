using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI = Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;
using System.Drawing;
using Autodesk.Revit.UI;

namespace RevitAddinBase.RevitControls
{
    public class Label : RibbonItemBase
    {
        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            throw new NotImplementedException();
        }

        public override RibbonItemData GetData(Dictionary<string, object> resources)
        {
            Text = (string)GetResx(resources, "_Button_caption");
            string name = CommandName;
            string assemblyName = AddinApplicationBase.Instance.ExecutingAssembly.Location;
            PushButtonData labelButtonData = new PushButtonData(name, Text, assemblyName, "_");
            return labelButtonData;
        }
    }
}
