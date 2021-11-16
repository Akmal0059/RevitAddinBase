using Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace RevitAddinBase.RevitControls
{
    public class ToggleButton : RevitCommandBase
    {
        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiToggleButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;

            AdWin.RibbonToggleButton ribbon = source.Items.FirstOrDefault(x => x.Id == Id) as AdWin.RibbonToggleButton;
            ribbon.LargeImage = GetImageSource((Bitmap)GetResx(resources, "_Button_image"));
            //ribbon.Description = (string)GetResx(resources, "_Button_long_description");
            object hideTextRes = GetResx(resources, "_Hide_text");
            ribbon.ShowText = hideTextRes == null ? true : !(bool)hideTextRes;
            ribbon.ToolTip = new AdWin.RibbonToolTip()
            {
                Title = (string)GetResx(resources, "_Button_caption"),
                Image = GetImageSource((Bitmap)GetResx(resources, "_Button_tooltip_image")),
                //Content = (string)GetResx(resources, "_Button_tooltip_text"),
            };
            return ribbon;
        }

        private void CreateRevitApiToggleButton(UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panels = app.GetRibbonPanels(AddinApplicationBase.TempTabName);
            var panel = panels.FirstOrDefault(x => x.Name == AddinApplicationBase.TempPanelName);
            if (panel == null)
                panel = app.CreateRibbonPanel(AddinApplicationBase.TempTabName, AddinApplicationBase.TempPanelName);

            string name = CommandName;
            string text = (string)GetResx(resources, "_Button_caption");
            string assemblyName = AddinApplicationBase.Instance.ExecutingAssembly.Location;
            string className = CommandName;
            ToggleButtonData toggleButtonData = new ToggleButtonData(name, text, assemblyName, className);
            var btn = panel.AddItem(toggleButtonData);
            string uriStr = (string)GetResx(resources, "_Help_file_name");
            if (!string.IsNullOrWhiteSpace(uriStr))
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string helpFilePath = $@"{appDataDir}\Inpad\Revit\HelpFiles\{uriStr}";
                btn.SetContextualHelp(new Autodesk.Revit.UI.ContextualHelp(ContextualHelpType.Url, helpFilePath));
            }
        }
    }
}
