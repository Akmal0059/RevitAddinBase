using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdWin = Autodesk.Windows;
using System.Xml.Serialization;
using Autodesk.Revit.UI;

namespace RevitAddinBase.RevitControls
{
    public abstract class RibbonItemBase
    {
        [XmlIgnore]
        public AdWin.RibbonItem RevitRibbonItem { get; set; }
        public string CommandName { get ; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string ContextualHelp { get; set; }
        public string AvailabilityClassName { get; set; }
        public string Text { get; set; }

        public abstract AdWin.RibbonItem CreateRibbon(UIControlledApplication app, Dictionary<string, object> resources);

    }
}
