using UI = Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;

namespace RevitAddinBase.RevitControls
{
    public class StackItem : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            AdWin.RibbonRowPanel ribbon = new AdWin.RibbonRowPanel();
            var firstItem = Items[0].CreateRibbon(app, resources, true);
            ribbon.Items.Add(firstItem);
            for (int i = 1; i < Items.Count; i++)
            {
                ribbon.Items.Add(new AdWin.RibbonRowBreak() { MinHeight = 0 });
                var ribbonItem = Items[i].CreateRibbon(app, resources, true);
                ribbon.Items.Add(ribbonItem);
            }

            return ribbon;
        }
    }
}
