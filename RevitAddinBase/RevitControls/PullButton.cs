using UI = Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;
using System.Drawing;

namespace RevitAddinBase.RevitControls
{
    public class PullButton : ButtonListBase
    {
        public string IconPath {  get; set; }

        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            CreateRevitApiSplitButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == "Addins");
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == "Temp").Source;

            AdWin.RibbonSplitButton ribbon = source.Items.FirstOrDefault(x => x.Name == CommandName) as AdWin.RibbonSplitButton;
            ribbon.Image = GetImageSource((Bitmap)resources[$"{CommandName}_Button_caption"]);
            ribbon.IsSplit = false;
            foreach (var item in Items)
                ribbon.Items.Add(item.CreateRibbon(app, resources));
            return ribbon;
        }

        private void CreateRevitApiSplitButton(UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(UI.Tab.AddIns).FirstOrDefault(x => x.Name == "Temp");
            if (panel == null)
                panel = app.CreateRibbonPanel(UI.Tab.AddIns, "Temp");

            string name = CommandName;
            string text = (string)resources[$"{CommandName}_Button_caption"];
            UI.PulldownButtonData pulldownData = new UI.PulldownButtonData(name, text);
            panel.AddItem(pulldownData);
        }
    }
}
