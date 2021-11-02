using RevitAddinBase.RevitControls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
        public List<RibbonPanel> Panels { get; set; }

        public RibbonTab()
        {
            Panels = new List<RibbonPanel>();
        }

        public RibbonTab(string name)
        {
            Name = name;
            Panels = new List<RibbonPanel>();
        }

        public Autodesk.Windows.RibbonTab CreateTab(Autodesk.Revit.UI.UIControlledApplication app, Dictionary<string, object> resources)
        {
            throw new NotImplementedException();
        }
    }
}
