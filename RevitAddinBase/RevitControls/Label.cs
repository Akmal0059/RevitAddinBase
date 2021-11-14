using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI = Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;
using System.Drawing;

namespace RevitAddinBase.RevitControls
{
    public class Label : RibbonItemBase
    {

        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            AdWin.RibbonLabel ribbon = new AdWin.RibbonLabel();
            ribbon.ShowText = true;
            ribbon.Text = (string)resources[$"{CommandName}_Button_caption"];
            return ribbon;
        }
    }
}
