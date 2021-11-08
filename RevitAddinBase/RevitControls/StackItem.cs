using Autodesk.Revit.UI;
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
    public class StackItem : ButtonListBase
    {
        public override AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources)
        {
            CreateRevitApiStackItem(app, resources);
            var control = AdWin.ComponentManager.Ribbon;
            var tempTab = control.Tabs.FirstOrDefault(x => x.Name == "Addins");
            var source = tempTab.Panels.FirstOrDefault(x => x.Source.Title == "Temp").Source;

            AdWin.RibbonRowPanel ribbon = source.Items.FirstOrDefault(x => x.Name == CommandName) as AdWin.RibbonRowPanel;
            ribbon.LargeImage = GetImageSource((Bitmap)resources[$"{CommandName}_Button_caption"]);
            ribbon.Items.Add(Items[0].CreateRibbon(app, resources));

            for (int i = 1; i < Items.Count; i++)
            {
                ribbon.Items.Add(new AdWin.RibbonRowBreak());
                ribbon.Items.Add(Items[i].CreateRibbon(app, resources));
            }

            return ribbon;
        }

        private void CreateRevitApiStackItem(UIControlledApplication app, Dictionary<string, object> resources)
        {
            var panel = app.GetRibbonPanels(Tab.AddIns).FirstOrDefault(x => x.Name == "Temp");
            if (panel == null)
                panel = app.CreateRibbonPanel(Tab.AddIns, "Temp");

            string name = CommandName;
            string text = (string)resources[$"{CommandName}_Button_caption"];
            string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string assemblyName = $@"{assemblyFolder}\InpadPlugins\InpadPlugins.dll";
            string className = CommandName;
            PushButtonData pushButtonData = new PushButtonData(name, text, assemblyName, className);
            panel.AddItem(pushButtonData);
        }
    }
}
