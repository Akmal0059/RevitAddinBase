using RevitAddinBase.RevitControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using AdWin = Autodesk.Windows;

namespace RevitAddinBase.RevitContainers
{
    #region Included types
    [XmlInclude(typeof(ToggleButton))]
    [XmlInclude(typeof(PushButton))]
    [XmlInclude(typeof(ArrowButton))]
    [XmlInclude(typeof(Textbox))]
    [XmlInclude(typeof(Label))]
    [XmlInclude(typeof(RadioGroup))]
    [XmlInclude(typeof(SplitButton))]
    [XmlInclude(typeof(StackItem))]
    [XmlInclude(typeof(PullButton))]
    [XmlInclude(typeof(ComboBox))]
    [XmlInclude(typeof(RibbonPanel))]
    [XmlInclude(typeof(Separator))]
    #endregion
    public class RibbonTab
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public List<RibbonPanel> Panels { get; set; }
        //public AdWin.RibbonTab AdWindowsRibbonTab { get; private set; }
        public RibbonTab()
        {
            Panels = new List<RibbonPanel>();
        }

        public RibbonTab(string name)
        {
            Name = name;
            Panels = new List<RibbonPanel>();
        }

        public AdWin.RibbonTab CreateTab(Autodesk.Revit.UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            app.CreateRibbonTab(Title);
            var control = Autodesk.Windows.ComponentManager.Ribbon;
            Autodesk.Windows.RibbonTab tab = control.Tabs.FirstOrDefault(x => x.Name == Title);
            //AdWindowsRibbonTab = tab;
            //tab settings

            //tab settings
            foreach (var panel in Panels)
            {
                var ribbonPanel = app.CreateRibbonPanel(Title, panel.Text);
                var createdPanel = panel.CreatePanel(app, resources, tab);
                //tab.Panels.Add(createdPanel);
            }
            
            return tab;
        }
    }
}
