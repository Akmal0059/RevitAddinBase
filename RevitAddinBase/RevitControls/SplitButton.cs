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
    public class SplitButton : ButtonListBase
    {
        public int? SelectedIndex { get; set; }

        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiSplitButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;
            SelectedIndex = (int?)GetResx(resources, "_SelectedIndex");

            AdWin.RibbonSplitButton ribbon = source.Items.FirstOrDefault(x => x.Id == Id) as AdWin.RibbonSplitButton;
            ribbon.IsSplit = true;
            foreach (var item in Items)
                ribbon.Items.Add(item.CreateRibbon(app, resources));
            if (SelectedIndex >= 0)
            {
                ribbon.Current = ribbon.Items[SelectedIndex.Value];
                ribbon.IsSynchronizedWithCurrentItem = false;
            }
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
            Text = (string)GetResx(resources, "_Button_caption");
            UI.SplitButtonData splitButtonData = new UI.SplitButtonData(name, Text);
            panel.AddItem(splitButtonData);
        }
    }
}
