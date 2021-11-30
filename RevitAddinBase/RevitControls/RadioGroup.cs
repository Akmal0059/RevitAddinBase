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
        public override AdWin.RibbonItem CreateRibbon(UI.UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiSplitButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;

            AdWin.RibbonRadioButtonGroup ribbon = source.Items.FirstOrDefault(x => x.Id == Id) as AdWin.RibbonRadioButtonGroup;
            ribbon.Image = GetImageSource((Bitmap)GetResx(resources, "_Button_caption"));
            foreach (var item in Items)
                ribbon.Items.Add(item.CreateRibbon(app, resources));
            return ribbon;
        }

        private void CreateRevitApiSplitButton(UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panels = app.GetRibbonPanels(AddinApplicationBase.TempTabName);
            var panel = panels.FirstOrDefault(x => x.Name == AddinApplicationBase.TempPanelName);
            if (panel == null)
                panel = app.CreateRibbonPanel(AddinApplicationBase.TempTabName, AddinApplicationBase.TempPanelName);

            string name = CommandName;
            UI.RadioButtonGroupData radioGrData = new UI.RadioButtonGroupData(name);
            var btn = panel.AddItem(radioGrData);
            string uriStr = (string)GetResx(resources, "_Help_file_name");
            if (!string.IsNullOrWhiteSpace(uriStr))
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string helpFilePath = $@"{appDataDir}\Inpad\Revit\HelpFiles\{uriStr}";
                if (File.Exists(helpFilePath))
                    btn.SetContextualHelp(new UI.ContextualHelp(UI.ContextualHelpType.Url, helpFilePath));
            }
        }
    }
}
