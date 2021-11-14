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
    public class ComboBox : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiCombobox(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == "Addins");
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == "Temp").Source;

            AdWin.RibbonCombo ribbon = source.Items.FirstOrDefault(x => x.Name == CommandName) as AdWin.RibbonCombo;
            foreach (var item in Items)
                ribbon.Items.Add(item.CreateRibbon(app, resources));
            return ribbon;
        }

        private void CreateRevitApiCombobox(UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(UI.Tab.AddIns).FirstOrDefault(x => x.Name == "Temp");
            if (panel == null)
                panel = app.CreateRibbonPanel(UI.Tab.AddIns, "Temp");

            string name = CommandName;
            string text = (string)resources[$"{CommandName}_Button_caption"];
            UI.ComboBoxData pulldownData = new UI.ComboBoxData(name);
            panel.AddItem(pulldownData);
        }
    }
}
