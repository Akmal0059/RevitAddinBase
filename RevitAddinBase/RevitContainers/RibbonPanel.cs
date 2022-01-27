using Autodesk.Revit.UI;
using Autodesk.Windows;
using RevitAddinBase.RevitControls;
using System.Collections.Generic;
using System.Linq;

namespace RevitAddinBase.RevitContainers
{
    public class RibbonPanel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public List<RibbonItemBase> Items { get; set; }
        //public Autodesk.Windows.RibbonPanel AdWindowsPanel { get; private set; }

        public Autodesk.Windows.RibbonPanel CreatePanel(UIControlledApplication app, Dictionary<string, object> resources, Autodesk.Windows.RibbonTab tab)
        {
            try
            {
                app.CreateRibbonPanel(tab.Title, Text);
            }
            catch { }
            Autodesk.Windows.RibbonPanel panel = tab.Panels.FirstOrDefault(x => x.Source.Title == Text);
            RibbonPanelSource source = panel.Source;
            //source settings

            //source settings

            foreach (var item in Items.Where(x => !x.IsSlideOut))
            {
                //source.Items.Add(item.CreateRibbon(app, resources, tab.Title, Text));
                item.CreateRibbon(app, resources, tab.Title, Text);
            }

            var slideOuts = Items.Where(x => x.IsSlideOut).ToList();

            if (slideOuts.Count > 0)
                source.Items.Add(new RibbonPanelBreak());
            foreach (var item in slideOuts)
            {
                //source.Items.Add(item.CreateRibbon(app, resources, tab.Title, Text));
                item.CreateRibbon(app, resources, tab.Title, Text);
            }
            panel.Source = source;

            return panel;
        }
    }
}
