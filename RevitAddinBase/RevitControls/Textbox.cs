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
            CreateRevitApiTextBox(app, resources, tabText, panelText);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == tabText);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == panelText).Source;

            AdWin.RibbonTextBox ribbon = source.Items.FirstOrDefault(x => x.Id == GetId(tabText, panelText)) as AdWin.RibbonTextBox;
            ribbon.ShowImageAsButton = true;
            ribbon.ImageLocation = AdWin.RibbonTextBoxImageLocation.InsideRight;
            ribbon.Image = GetImageSource((Bitmap)GetResx(resources, "_Button_image"));
            return ribbon;
        }

        private void CreateRevitApiTextBox(UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText)
        {
            var panel = app.GetRibbonPanels(tabText).FirstOrDefault(x => x.Name == panelText);
            if (panel == null)
                panel = app.CreateRibbonPanel(tabText, panelText);
            Text = (string)GetResx(resources, "_Button_caption");
            string name = CommandName;
            TextBoxData tbData = new TextBoxData(name);
            panel.AddItem(tbData);
        }
    }
}
