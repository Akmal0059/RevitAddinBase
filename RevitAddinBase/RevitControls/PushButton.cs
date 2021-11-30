using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using AdWin = Autodesk.Windows;

namespace RevitAddinBase.RevitControls
{
    public class PushButton : RevitCommandBase
    {
        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources, bool isStacked = false)
        {
            CreateRevitApiButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;

            AdWin.RibbonButton ribbon = source.Items.FirstOrDefault(x => x.Id == Id) as AdWin.RibbonButton;
            ribbon.LargeImage = GetImageSource((Bitmap)GetResx(resources, "_Button_image"));
            ribbon.Image = GetImageSource((Bitmap)GetResx(resources, "_Button_image"));
            //ribbon.Description = (string)GetResx(resources, "_Button_long_description");
            ribbon.IsToolTipEnabled = true;

            object hideTextRes = GetResx(resources, "_Hide_text");
            ribbon.ShowText = hideTextRes == null ? true : !(bool)hideTextRes;
            ribbon.ToolTip = new AdWin.RibbonToolTip()
            {
                Title = (string)GetResx(resources, "_Button_caption"),
                Image = GetImageSource((Bitmap)GetResx(resources, "_Button_tooltip_image")),
                //Content = (string)GetResx(resources, "_Button_tooltip_text"),
            };

            if (isStacked)
            {
                ribbon.Orientation = System.Windows.Controls.Orientation.Horizontal;
                ribbon.Size = AdWin.RibbonItemSize.Standard;
            }
            return ribbon;
        }

        private void CreateRevitApiButton(UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panels = app.GetRibbonPanels(AddinApplicationBase.TempTabName);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == AddinApplicationBase.TempTabName);
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == AddinApplicationBase.TempPanelName).Source;

            var panel = panels.FirstOrDefault(x => x.Name == AddinApplicationBase.TempPanelName);
            if (panel == null)
                panel = app.CreateRibbonPanel(AddinApplicationBase.TempTabName, AddinApplicationBase.TempPanelName);
            Text = (string)GetResx(resources, "_Button_caption");
            string name = CommandName;
            string assemblyName = AddinApplicationBase.Instance.ExecutingAssembly.Location;
            string className = CommandName;
            PushButtonData pushButtonData = new PushButtonData(name, Text, assemblyName, className);
            var btn = panel.AddItem(pushButtonData);
            string uriStr = (string)GetResx(resources, "_Help_file_name");

            if (!string.IsNullOrWhiteSpace(uriStr))
            {
                string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string helpFilePath = $@"{appDataDir}\Inpad\Revit\HelpFiles\{uriStr}";
                if (File.Exists(helpFilePath))
                    btn.SetContextualHelp(new Autodesk.Revit.UI.ContextualHelp(ContextualHelpType.Url, helpFilePath));
            }
        }
    }
}
