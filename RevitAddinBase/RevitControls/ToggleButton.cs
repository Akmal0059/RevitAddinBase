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
        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources)
        {
            CreateRevitApiToggleButton(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == "Addins");
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == "Temp").Source;

            AdWin.RibbonToggleButton toggle = source.Items.FirstOrDefault(x => x.Name == CommandName) as AdWin.RibbonToggleButton;
            toggle.LargeImage = GetImageSource((Bitmap)resources[$"{CommandName}_Button_caption"]);
            return toggle;
        }

        private void CreateRevitApiToggleButton(UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(Tab.AddIns).FirstOrDefault(x => x.Name == "Temp");
            if (panel == null)
                panel = app.CreateRibbonPanel(Tab.AddIns, "Temp");

            string name = CommandName;
            string text = (string)resources[$"{CommandName}_Button_caption"];
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assemblyName = $@"{assemblyFolder}\InpadPlugins\InpadPlugins.dll";
            string className = CommandName;
            ToggleButtonData toggleButtonData = new ToggleButtonData(name, text, assemblyName, className);
            panel.AddItem(toggleButtonData);
        }
    }
}
