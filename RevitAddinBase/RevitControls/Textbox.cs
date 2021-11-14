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

        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiTextBox(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;

            AdWin.RibbonTextBox ribbon = source.Items.FirstOrDefault(x => x.Id == Id) as AdWin.RibbonTextBox;
            ribbon.ShowImageAsButton = true;
            ribbon.ImageLocation = AdWin.RibbonTextBoxImageLocation.InsideRight;
            ribbon.Image = GetImageSource((Bitmap)resources[$"{CommandName}_Button_image"]);
            return ribbon;
        }

        private void CreateRevitApiTextBox(UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(AddinApplicationBase.TempTabName).FirstOrDefault(x => x.Name == AddinApplicationBase.TempPanelName);
            if (panel == null)
                panel = app.CreateRibbonPanel(AddinApplicationBase.TempTabName, AddinApplicationBase.TempPanelName);
            Text = (string)resources[$"{CommandName}_Button_caption"];
            string name = CommandName;
            TextBoxData tbData = new TextBoxData(name);
            panel.AddItem(tbData);
        }
    }
}
