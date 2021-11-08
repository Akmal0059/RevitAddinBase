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

        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources)
        {
            CreateRevitApiTextBox(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == "Addins");
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == "Temp").Source;

            AdWin.RibbonTextBox ribbon = source.Items.FirstOrDefault(x => x.Name == CommandName) as AdWin.RibbonTextBox;
            ribbon.Image = GetImageSource((Bitmap)resources[$"{CommandName}_Button_caption"]);
            return ribbon;
        }

        private void CreateRevitApiTextBox(UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(Tab.AddIns).FirstOrDefault(x => x.Name == "Temp");
            if (panel == null)
                panel = app.CreateRibbonPanel(Tab.AddIns, "Temp");

            string name = CommandName;
            TextBoxData tbData = new TextBoxData(name);
            panel.AddItem(tbData);
        }
    }
}
