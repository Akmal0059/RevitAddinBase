using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI = Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using RevitAddinBase.RevitControls.Model;

namespace RevitAddinBase.RevitControls
{
    public class ComboBox : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            CreateRevitApiCombobox(app, resources, tabText, panelText);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == tabText);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == panelText).Source;
            AdWin.RibbonCombo ribbon = source.Items.FirstOrDefault(x => x.Id == GetId(tabText, panelText)) as AdWin.RibbonCombo;

            foreach (var item in Items)
                ribbon.Items.Add(item.Text);
            return ribbon;
        }

        private void CreateRevitApiCombobox(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText)
        {
            var panels = app.GetRibbonPanels(tabText);
            var panel = panels.FirstOrDefault(x => x.Name == panelText);
            if (panel == null)
                panel = app.CreateRibbonPanel(tabText, panelText);

            string name = CommandName;
            string text = (string)GetResx(resources, "_Button_caption");
            UI.ComboBoxData pulldownData = new UI.ComboBoxData(name);
            panel.AddItem(pulldownData);
        }
        public override UI.RibbonItemData GetData(Dictionary<string, object> resources)
        {
            string name = CommandName;
            UI.ComboBoxData cboboboxData = new UI.ComboBoxData(name);
            return cboboboxData;
        }
        public virtual void AddEventHandlers(AdWin.RibbonCombo control)
        {

        }

        public virtual void SetDataTemplate(AdWin.RibbonCombo combo)
        {
            
        }
    }
}
