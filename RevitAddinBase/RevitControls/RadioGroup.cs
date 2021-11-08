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
    public class RadioGroup : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            CreateRevitApiSplitButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == "Addins");
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == "Temp").Source;

            AdWin.RibbonRadioButtonGroup ribbon = source.Items.FirstOrDefault(x => x.Name == CommandName) as AdWin.RibbonRadioButtonGroup;
            ribbon.Image = GetImageSource((Bitmap)resources[$"{CommandName}_Button_caption"]);
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
            UI.RadioButtonGroupData radioGrData = new UI.RadioButtonGroupData(name);
            panel.AddItem(radioGrData);
        }
    }
}
