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

        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiSplitButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;

            AdWin.RibbonSplitButton ribbon = source.Items.FirstOrDefault(x => x.Id == Id) as AdWin.RibbonSplitButton;
            ribbon.Image = GetImageSource((Bitmap)resources[$"{CommandName}_Button_image"]);
            ribbon.LargeImage = GetImageSource((Bitmap)resources[$"{CommandName}_Button_image"]);
            ribbon.IsSplit = false;
            foreach (var item in Items)
                ribbon.Items.Add(item.CreateRibbon(app, resources));
            
            if (isStacked)
            {
                ribbon.Orientation = System.Windows.Controls.Orientation.Horizontal;
                ribbon.Size = AdWin.RibbonItemSize.Standard;
            }
            return ribbon;
        }

        private void CreateRevitApiSplitButton(UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(AddinApplicationBase.TempTabName).FirstOrDefault(x => x.Name == AddinApplicationBase.TempPanelName);
            if (panel == null)
                panel = app.CreateRibbonPanel(AddinApplicationBase.TempTabName, AddinApplicationBase.TempPanelName);

            string name = CommandName;
            Text = (string)resources[$"{CommandName}_Button_caption"];
            UI.PulldownButtonData pulldownData = new UI.PulldownButtonData(name, Text);
            panel.AddItem(pulldownData);
        }
    }
}
