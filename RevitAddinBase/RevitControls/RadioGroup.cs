using UI = Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;
using System.Drawing;
using System.IO;

namespace RevitAddinBase.RevitControls
{
    public class RadioGroup : ButtonListBase
    {
        public string GroupName { get; set; }
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText, bool isStacked = false)
        {
            CreateRevitApiSplitButton(app, resources, tabText, panelText);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == tabText);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == panelText).Source;

            AdWin.RibbonRadioButtonGroup ribbon = source.Items.FirstOrDefault(x => x.Id == GetId(tabText, panelText)) as AdWin.RibbonRadioButtonGroup;
            //ribbon.Image = GetImageSource((Bitmap)GetResx(resources, "_Button_caption"));
            //foreach (var item in Items)
            //    ribbon.Items.Add(item.CreateRibbon(app, resources, tabText, panelText));
            for (int i = 0; i < ribbon.Items.Count; i++)
            {
                AdWin.RibbonToggleButton item = ribbon.Items[i] as AdWin.RibbonToggleButton;
                //item.Image = GetImageSource((Bitmap)Items[i].GetResx(resources, "_Button_image"));
            }
            return ribbon;
        }

        public override UI.RibbonItemData GetData(Dictionary<string, object> resources)
        {
            throw new NotImplementedException();
        }

        private void CreateRevitApiSplitButton(UI.UIControlledApplication app, Dictionary<string, object> resources, string tabText, string panelText)
        {
            var panels = app.GetRibbonPanels(tabText);
            var panel = panels.FirstOrDefault(x => x.Name == panelText);
            if (panel == null)
                panel = app.CreateRibbonPanel(tabText, panelText);

            string name = CommandName;
            UI.RadioButtonGroupData radioGrData = new UI.RadioButtonGroupData(name);
            var radioGroup = panel.AddItem(radioGrData) as UI.RadioButtonGroup;
            foreach (var item in Items)
            {
                radioGroup.AddItem(item.GetData(resources) as UI.ToggleButtonData);
            }
            string uriStr = (string)GetResx(resources, "_Help_file_name");
            if (!string.IsNullOrWhiteSpace(uriStr))
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string helpFilePath = $@"{appDataDir}\Inpad\Revit\HelpFiles\{uriStr}";
                if (File.Exists(helpFilePath))
                    radioGroup.SetContextualHelp(new UI.ContextualHelp(UI.ContextualHelpType.Url, helpFilePath));
            }
        }
    }
}
